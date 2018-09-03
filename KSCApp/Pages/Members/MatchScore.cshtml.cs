using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using KSCApp.Models;
using KSCApp.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace KSCApp.Pages.Members
{
    public class MatchScoreModel : PageModel
    {
        private readonly KSCApp.Data.ApplicationDbContext _context;

        public MatchScoreModel(KSCApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ResultsVM resultsVM { get; set; }

        [BindProperty]
        public Match Match { get; set; }


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id!=null)
            {

               Match = await _context.Match
                    .Include(m=>m.Fixture)
                    .Include(m=>m.Fixture.League)
                    .Include(m=>m.PlayerA)
                    .Include(m=>m.PlayerB)
                    .FirstOrDefaultAsync(m=>m.MatchId == id);

                resultsVM = new ResultsVM() { MatchId = Match.MatchId };

                if (Match.Played == true)
                {
                    return RedirectToPage("./Index");
                }
                

            }
            else
            {
                return RedirectToPage("./Index");
            }


            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }


            if (resultsVM != null)
            {
                Match = _context.Match
                     .Include(m => m.Fixture)
                     .Include(m => m.Fixture.League)
                     .Include(m => m.PlayerA)
                     .Include(m => m.PlayerB)
                     .FirstOrDefault(m => m.MatchId == resultsVM.MatchId);

                if (Match.Played == true)
                {
                    return Page();
                }


                var teamPlayerA = _context.TeamPlayer.FirstOrDefault(tp => tp.PlayerId == Match.PlayerAId && tp.TeamId == Match.Fixture.TeamAId);

                var teamPlayerB = _context.TeamPlayer.FirstOrDefault(tp => tp.PlayerId == Match.PlayerBId && tp.TeamId == Match.Fixture.TeamBId);

                int gamesPlayerA = 0;

                int gamesPlayerB = 0;

                //Make new result for Game 1
                if (resultsVM.Game1PlayerAScore + resultsVM.Game1PlayerBScore < 1)
                    return Page();
                else
                {
                    try
                    {
                        if (resultsVM.Game1PlayerAScore > resultsVM.Game1PlayerBScore)
                            gamesPlayerA += 1;
                        else
                            gamesPlayerB += 1;

                        GameResult gameResult = new GameResult()
                        {
                            MatchId = Match.MatchId,
                            GameNo = 1,
                            PlayerApoints = resultsVM.Game1PlayerAScore,
                            PlayerBpoints = resultsVM.Game1PlayerBScore
                        };
                        _context.GameResult.Add(gameResult);
                        _context.SaveChanges();
                    }
                    catch
                    {
                        throw;
                    }
                }

                //Make new result for Game 2
                if (resultsVM.Game2PlayerAScore + resultsVM.Game2PlayerBScore > 0)
                {
                    try
                    {
                        if (resultsVM.Game2PlayerAScore > resultsVM.Game2PlayerBScore)
                            gamesPlayerA += 1;
                        else
                            gamesPlayerB += 1;

                        GameResult gameResult = new GameResult()
                        {
                            MatchId = Match.MatchId,
                            GameNo = 2,
                            PlayerApoints = resultsVM.Game2PlayerAScore,
                            PlayerBpoints = resultsVM.Game2PlayerBScore
                        };
                        _context.GameResult.Add(gameResult);
                        _context.SaveChanges();
                    }
                    catch
                    {
                        throw;
                    }
                }

                //Make new result for Game 3
                if (resultsVM.Game3PlayerAScore + resultsVM.Game3PlayerBScore > 0)
                {
                    try
                    {
                        if (resultsVM.Game3PlayerAScore > resultsVM.Game3PlayerBScore)
                            gamesPlayerA += 1;
                        else
                            gamesPlayerB += 1;

                        GameResult gameResult = new GameResult()
                        {
                            MatchId = Match.MatchId,
                            GameNo = 3,
                            PlayerApoints = resultsVM.Game3PlayerAScore,
                            PlayerBpoints = resultsVM.Game3PlayerBScore
                        };
                        _context.GameResult.Add(gameResult);
                        _context.SaveChanges();
                    }
                    catch
                    {
                        throw;
                    }
                }

                //Make new result for Game 4
                if (resultsVM.Game4PlayerAScore + resultsVM.Game4PlayerBScore > 0)
                {
                    try
                    {
                        if (resultsVM.Game4PlayerAScore > resultsVM.Game4PlayerBScore)
                            gamesPlayerA += 1;
                        else
                            gamesPlayerB += 1;

                        GameResult gameResult = new GameResult()
                        {
                            MatchId = Match.MatchId,
                            GameNo = 4,
                            PlayerApoints = resultsVM.Game4PlayerAScore,
                            PlayerBpoints = resultsVM.Game4PlayerBScore
                        };
                        _context.GameResult.Add(gameResult);
                        _context.SaveChanges();
                    }
                    catch
                    {
                        throw;
                    }
                }

                //Make new result for Game 5
                if (resultsVM.Game5PlayerAScore + resultsVM.Game5PlayerBScore > 0)
                {
                    try
                    {
                        if (resultsVM.Game5PlayerAScore > resultsVM.Game5PlayerBScore)
                            gamesPlayerA += 1;
                        else
                            gamesPlayerB += 1;

                        GameResult gameResult = new GameResult()
                        {
                            MatchId = Match.MatchId,
                            GameNo = 5,
                            PlayerApoints = resultsVM.Game5PlayerAScore,
                            PlayerBpoints = resultsVM.Game5PlayerBScore
                        };
                        _context.GameResult.Add(gameResult);
                        _context.SaveChanges();
                    }
                    catch
                    {
                        throw;
                    }
                }


                //Update Teamplayer A with match result
                try
                {
                    teamPlayerA.GamesWon += gamesPlayerA;
                    teamPlayerA.GamesLost += gamesPlayerB;
                    if (gamesPlayerA > gamesPlayerB)
                        teamPlayerA.MatchesWon += 1;
                    teamPlayerA.MatchesPlayed += 1;

                    _context.TeamPlayer.Update(teamPlayerA);
                    await _context.SaveChangesAsync();
                }
                catch
                {
                    throw;
                }

                //Update Teamplayer B with match result
                try
                {
                    teamPlayerB.GamesWon += gamesPlayerB;
                    teamPlayerB.GamesLost += gamesPlayerA;
                    if (gamesPlayerB > gamesPlayerA)
                        teamPlayerB.MatchesWon += 1;
                    teamPlayerB.MatchesPlayed += 1;

                    _context.TeamPlayer.Update(teamPlayerB);
                    await _context.SaveChangesAsync();
                }
                catch
                {
                    throw;
                }

                //Update Match with match result and mark as played
                try
                {
                    Match.Played = true;
                    Match.PlayedDate = DateTime.Today;
                    Match.PlayerAgames = gamesPlayerA;
                    Match.PlayerBgames = gamesPlayerB;
                    _context.Match.Update(Match);


                    //If result is for future scheduled match, free up the slot
                    var matchslot = _context.MatchSlot.Where(ms => ms.MatchId == resultsVM.MatchId && ms.BookingSlot > DateTime.Now)
                        .FirstOrDefault();

                    if (matchslot != null)
                    {
                        matchslot.MatchId = null;
                        _context.MatchSlot.Update(matchslot);
                    }
                        
                    await _context.SaveChangesAsync();
                }
                catch
                {
                    throw;
                }

            }

            return RedirectToPage("../Index");
        }
    }
}