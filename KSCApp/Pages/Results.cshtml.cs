using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using KSCApp.Models;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using KSCApp.ViewModels;

namespace KSCApp.Pages
{
    public class ResultsModel : PageModel
    {
        private readonly KSCApp.Data.ApplicationDbContext _context;

        public ResultsModel(KSCApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<MatchResultVM> ResultList { get; set; }

        [BindProperty]
        public League SelectedLeague { get; set; }

        public async Task OnGetAsync()
        {
            ViewData["LeagueId"] = new SelectList(_context.League.Where(l=>l.Active==true), "LeagueId", "LeagueName");

            string tempLeagueString = HttpContext.Session.GetString("SelectedLeague");

            int CurrentLeagueId = 0;


            if (tempLeagueString != null)
            {
                CurrentLeagueId = Convert.ToInt32(tempLeagueString);
                SelectedLeague = _context.League.FirstOrDefault(l => l.LeagueId == CurrentLeagueId && l.Active == true);
            }

            if (SelectedLeague == null)
                SelectedLeague = _context.League.OrderByDescending(l => l.LeagueId).FirstOrDefault(l => l.Active == true);

            ResultList = await _context.Match.Where(m => m.PlayedDate != null && m.Fixture.LeagueId == SelectedLeague.LeagueId)
                .Include(m => m.Fixture)
                .Include(m => m.Fixture.TeamA)
                .Include(m => m.Fixture.TeamB)
                .Include(m => m.PlayerA)
                .Include(m => m.PlayerB)
                .Include(m => m.GameResults)
                .Select(u => new MatchResultVM
                {
                    FixtureDetails = u.Fixture.TeamA.TeamName + " v " + u.Fixture.TeamB.TeamName,
                    MatchDetails = String.Format("{0} ({1}) v {2} ({3})", u.PlayerA.PlayerName, u.PlayerAgames, u.PlayerB.PlayerName, u.PlayerBgames),
                    GameResults = "",
                    MatchId = u.MatchId,
                    DatePlayed = (u.PlayedDate ?? DateTime.Now).ToString("dd/MM/yyy")
                })
                .OrderByDescending(u=>u.DatePlayed)
                .AsNoTracking()
                .ToListAsync();

            foreach (var res in ResultList)
            {
                string gameResults = "(";
                var games = _context.GameResult.Where(g => g.MatchId == res.MatchId);
                foreach (var game in games)
                {
                    gameResults += string.Format(" {0}-{1},", game.PlayerApoints, game.PlayerBpoints);
                }
                gameResults = gameResults.Remove(gameResults.Length - 1);
                gameResults += " )";
                res.GameResults = gameResults;
            }
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }


            HttpContext.Session.SetString("SelectedLeague", SelectedLeague.LeagueId.ToString());

            return RedirectToPage("./Results");
        }
    }
}