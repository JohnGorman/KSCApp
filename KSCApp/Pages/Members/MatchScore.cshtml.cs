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

        [BindProperty]
        public ResultsVM ResultsVM { get; set; }

        [BindProperty]
        public List<Game> Games { get; set; }

        [BindProperty]
        public Match Match { get; set; }

        public MatchScoreModel(KSCApp.Data.ApplicationDbContext context)
        {
            _context = context;          
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id!=null)
            {
                Match = await GetMatch(id);

                ResultsVM = new ResultsVM() { MatchId = Match.MatchId };

                this.Games = new List<Game>();

                //Create 5 new games for result entry
                for (int i = 0; i < 5; i++)
                {
                    var game = new Game();
                    game.GameNo = i + 1;
                    Games.Add(game);
                }


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

        public async Task<Match> GetMatch(int? id)
        {
            return await _context.Match
                    .Include(m => m.Fixture)
                    .Include(m => m.Fixture.League)
                    .Include(m => m.PlayerA)
                    .Include(m => m.PlayerB)
                    .FirstOrDefaultAsync(m => m.MatchId == id);
        }

        public async Task<TeamPlayer> GetTeamPlayer(int playerId, int teamId)
        {
            return await _context.TeamPlayer.FirstOrDefaultAsync(tp => tp.PlayerId == playerId && tp.TeamId == teamId);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (ResultsVM != null)
            {
                Match = await GetMatch(ResultsVM.MatchId);

                if (Match.Played == true)
                {
                    return Page();
                }

                TeamPlayer teamPlayerA = await GetTeamPlayer(Match.PlayerAId, Match.Fixture.TeamAId);
                TeamPlayer teamPlayerB = await GetTeamPlayer(Match.PlayerBId, Match.Fixture.TeamBId);

                int gamesPlayerA = 0;

                int gamesPlayerB = 0;

                //Iterate through all 5 games
                for (int i=0;i<5;i++)
                {
                    if (Games[i].PlayerAScore + Games[i].PlayerBScore > 0)
                    {
                        if (Games[i].PlayerAScore > Games[i].PlayerBScore)
                        {
                            gamesPlayerA++;
                        }
                        else
                        {
                            gamesPlayerB++;
                        }

                        GameResult gameResult = new GameResult()
                        {
                            MatchId = Match.MatchId,
                            GameNo = i+1,
                            PlayerApoints = Games[i].PlayerAScore,
                            PlayerBpoints = Games[i].PlayerBScore
                        };
                        await _context.GameResult.AddAsync(gameResult);
                    }
                }
                
                try
                {
                    //Update Teamplayer A with match result
                    teamPlayerA.GamesWon += gamesPlayerA;
                    teamPlayerA.GamesLost += gamesPlayerB;
                    if (gamesPlayerA > gamesPlayerB)
                        teamPlayerA.MatchesWon += 1;
                    teamPlayerA.MatchesPlayed += 1;
                    _context.TeamPlayer.Update(teamPlayerA);

                    //Update Teamplayer B with match result
                    teamPlayerB.GamesWon += gamesPlayerB;
                    teamPlayerB.GamesLost += gamesPlayerA;
                    if (gamesPlayerB > gamesPlayerA)
                        teamPlayerB.MatchesWon += 1;
                    teamPlayerB.MatchesPlayed += 1;
                    _context.TeamPlayer.Update(teamPlayerB);

                    //Update Match with match result and mark as played
                    Match.Played = true;
                    Match.PlayedDate = DateTime.Today;
                    Match.PlayerAgames = gamesPlayerA;
                    Match.PlayerBgames = gamesPlayerB;
                    _context.Match.Update(Match);

                    //If result is for future scheduled match, free up the slot
                    var matchslot = _context.MatchSlot.Where(ms => ms.MatchId == ResultsVM.MatchId && ms.BookingSlot > DateTime.Now)
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