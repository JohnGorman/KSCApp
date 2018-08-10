using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using KSCApp.Data;
using KSCApp.Models;
using KSCApp.ViewModels;

namespace KSCApp.Pages.Admin.Leagues
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
            ErrorString = new ErrorMessageVM();
            ErrorString.ErrorNo = 0;
            ErrorString.ErrorMessage = "";
            return Page();
        }

        [BindProperty]
        public League League { get; set; }
        [BindProperty]
        public ErrorMessageVM ErrorString { get; set; }



        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                _context.League.Add(League);
                _context.SaveChanges();

                //Get new league Id
                int currentLeagueId = League.LeagueId;

                //Create 8 new teams
                string section = "A";
                for (int i = 1; i < 9; i++)
                {
                    //If short league last 4 teams are in section b
                    if (League.LeagueType == LeagueType.Short && i > 4)
                    {
                        section = "B";
                    }
                    Team team = new Team { TeamNo = i, TeamName = "Team No " + i, Section = section, LeagueId = League.LeagueId };
                    _context.Team.Add(team);
                }

                //save new teams
                _context.SaveChanges();

                int playersRequired = League.NoOfLevels * 8;

                //new array to store the IDs of the new teams
                var newTeams = _context.Team.Where(l => l.LeagueId == League.LeagueId).ToArray();

                //Get an array list of all players active and playing league
                var LeaguePlayers = _context.Player.Where(p => p.PlayerStatus == PlayerStatusEnum.Active && p.PlayingLeague == true)
                                                    .OrderBy(l => l.Rank)
                                                    .ToArray();

                //check if enough players for league format
                if (LeaguePlayers.Length < playersRequired)
                {
                    ErrorString.ErrorNo = 1001;
                    ErrorString.ErrorMessage = "Not enough players to fill all levels";
                    return Page();
                }
                else
                {
                    //Fill a temporary 2D array with team and players
                    int[,] TeamArray = new int[League.NoOfLevels+1, 8];

                    int counter = 0;
                    for (int level = 1; level <= League.NoOfLevels; level++)
                    {
                        for (int teamNo = 0; teamNo < 8; teamNo++)
                        {
                            TeamArray[level, teamNo] = LeaguePlayers[counter].PlayerId;
                            counter++;
                        }
                    }


                    //Randomly shuffle the players on each level using 
                    Random randomFactory = new Random();
                    int randomSpot;
                    int tempId;

                    for (int level = 1; level <= League.NoOfLevels; level++)
                    {
                        for (int teamNo = 0; teamNo < 8; teamNo++)
                        {
                            randomSpot = randomFactory.Next(0, 8);
                            tempId = TeamArray[level, teamNo];
                            TeamArray[level, teamNo] = TeamArray[level, randomSpot];
                            TeamArray[level, randomSpot] = tempId;
                        }
                    }



                    //Create new teamplayers from temporary array
                    for (int level = 1; level <= League.NoOfLevels; level++)
                    {
                        for (int teamNo = 0; teamNo < 8; teamNo++)
                        {
                            TeamPlayer newTeamPlayer = new TeamPlayer {
                                PlayerId = TeamArray[level, teamNo],
                                TeamId = newTeams[teamNo].TeamId,
                                Level = level,
                                MatchesPlayed = 0,
                                MatchesWon = 0,
                                GamesWon = 0,
                                GamesLost = 0
                        };
                            _context.TeamPlayer.Add(newTeamPlayer);
                        }
                    }

                    _context.SaveChanges();

                    return RedirectToPage("./Index");
                }
            }

            catch
            {
                ErrorString.ErrorMessage = "Something went wrong!";
                ErrorString.ErrorNo = 1000;
                return Page();
            }

        }
                    





        
    }
}