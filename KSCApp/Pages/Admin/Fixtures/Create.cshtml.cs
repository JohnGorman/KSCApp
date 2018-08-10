using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using KSCApp.Data;
using KSCApp.Models;
using Microsoft.EntityFrameworkCore;
using KSCApp.ViewModels;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;


namespace KSCApp.Pages.Admin.Fixtures
{
    public class CreateModel : PageModel
    {
        private readonly KSCApp.Data.ApplicationDbContext _context;

        public CreateModel(KSCApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["LeagueId"] = new SelectList(_context.League, "LeagueId", "LeagueName");
        //ViewData["TeamAId"] = new SelectList(_context.Team, "TeamId", "TeamName");
        //ViewData["TeamBId"] = new SelectList(_context.Team, "TeamId", "TeamName");
            return Page();
        }

        [BindProperty]
        public int SelectedLeagueId { get; set; }


        //public Fixture Fixture { get; set; }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }


            //Get template of fixtures for league type
            var fixtureList = new List<FixtureDate>();

            //get current league using session data

            try
            {
                //string tempLeagueString = HttpContext.Session.GetString("SelectedLeague");

                //int CurrentLeagueId = Convert.ToInt32(tempLeagueString);
                League League = _context.League.FirstOrDefault(c => c.LeagueId == SelectedLeagueId);

                if (League.FixturesMade)
                {
                    return RedirectToPage("./Index");
                }


                if (League.LeagueType == LeagueType.Long)
                {
                    fixtureList = _context.FixtureDate.Where(fd => fd.LeagueType == LeagueType.Long)
                                            .OrderBy(fd => fd.StartDaysPlus)
                                            .ToList();
                }
                else
                {
                    fixtureList = _context.FixtureDate.Where(fd => fd.LeagueType == LeagueType.Short)
                            .OrderBy(fd => fd.StartDaysPlus)
                            .ToList();
                }


                //Get an array of the teams in this league
                var teamList = _context.Team.Where(t => t.LeagueId == League.LeagueId)
                    .OrderBy(t => t.TeamNo)
                    .ToArray();


                //Loop through and make new fixtures
                foreach (FixtureDate fd in fixtureList)
                {
                    Fixture newFixture = new Fixture
                    {
                        League = League,
                        TeamAId = teamList[fd.TeamANo - 1].TeamId,
                        TeamBId = teamList[fd.TeamBNo - 1].TeamId,
                        PlayDate = League.StartDate.AddDays(fd.StartDaysPlus)
                    };
                    _context.Fixture.Add(newFixture);
                    _context.SaveChanges();

                    //An array of team A players
                    var teamA = _context.TeamPlayer.Where(tp => tp.TeamId == newFixture.TeamAId)
                        .OrderBy(tp => tp.Level)
                        .ToArray();

                    //An array of team B players
                    var teamB = _context.TeamPlayer.Where(tp => tp.TeamId == newFixture.TeamBId)
                        .OrderBy(tp => tp.Level)
                        .ToArray();

                    //Loop through how many levels in this league
                    for (int level = 0; level < League.NoOfLevels; level++)
                    {
                        //get the playing time leveltime for this level
                        LevelTime thisLevelTime = _context.LevelTime.FirstOrDefault(lt => lt.Level == level + 1);

                        //Create a new match between opposing players at this level
                        Match newMatch = new Match
                        {
                            FixtureId = newFixture.FixtureId,
                            PlayerAId = teamA[level].PlayerId,
                            PlayerBId = teamB[level].PlayerId,
                            Level = level + 1,
                            Played = false,
                        };
                        _context.Match.Add(newMatch);
                        _context.SaveChanges();

                        //Create new MatchSlot and assign this match to it for this scheduled date and time
                        MatchSlot newMatchSlot = new MatchSlot
                        {
                            MatchId = newMatch.MatchId,
                            BookingSlot = newFixture.PlayDate + thisLevelTime.StartTime
                        };
                        _context.MatchSlot.Add(newMatchSlot);
                        _context.SaveChanges();
                    }

                }

                //Change fistures made to true
                League.FixturesMade = true;
                _context.SaveChanges();

                return RedirectToPage("./Index");
            }
            catch
            {
                return RedirectToPage("./Index");
            }



        }
    }
}