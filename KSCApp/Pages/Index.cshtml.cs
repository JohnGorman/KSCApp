using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using KSCApp.Models;
using KSCApp.ViewModels;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;

namespace KSCApp.Pages
{
    public class IndexModel : PageModel
    {
        public IActionResult OnGet()
        {
            ViewData["LeagueId"] = new SelectList(_context.League, "LeagueId", "LeagueName");

            string tempLeagueString = HttpContext.Session.GetString("SelectedLeague");

            int CurrentLeagueId = 0;

            if (tempLeagueString == null)
            {
                SelectedLeague = _context.League.OrderByDescending(c => c.LeagueId).FirstOrDefault();
                CurrentLeagueId = SelectedLeague.LeagueId;
            }
            else
            {
                CurrentLeagueId = Convert.ToInt32(tempLeagueString);
                SelectedLeague = _context.League.FirstOrDefault(c => c.LeagueId == CurrentLeagueId);
            }

            

            LeagueTable = (from Team in _context.Team
                         join TeamPlayer in _context.TeamPlayer on Team.TeamId equals TeamPlayer.TeamId into tvm
                         from con in tvm.DefaultIfEmpty()
                         select new TeamVM
                         {
                             LeagueId = Team.LeagueId,
                             TeamId = Team.TeamId,
                             TeamName = Team.TeamName,
                             TeamNo = Team.TeamNo,
                             Section = Team.Section,
                             MatchesPlayed = con.MatchesPlayed,
                             MatchesWon = con.MatchesWon,
                             GamesWon = con.GamesWon,
                             GamesLost = con.GamesLost
                         }).GroupBy(t=>t.TeamId)
                         .Select(ct=> new TeamVM
                         {
                             LeagueId = ct.First().LeagueId,
                             TeamId = ct.First().TeamId,
                             TeamName = ct.First().TeamName,
                             TeamNo = ct.First().TeamNo,
                             Section = ct.First().Section,
                             MatchesPlayed = ct.Sum(c => c.MatchesPlayed),
                             MatchesWon = ct.Sum(c => c.MatchesWon),
                             GamesWon = ct.Sum(c => c.GamesWon),
                             GamesLost = ct.Sum(c => c.GamesLost)
                         }
                         ).Where(c=>c.LeagueId == CurrentLeagueId)
                         .OrderByDescending(c=>c.GamesWon)
                         .ToList();

            return Page();
        }

        [BindProperty]
        public League SelectedLeague { get; set; }


        private readonly KSCApp.Data.ApplicationDbContext _context;

        public IndexModel(KSCApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<TeamVM> LeagueTable { get; set; }


        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }


            HttpContext.Session.SetString("SelectedLeague", SelectedLeague.LeagueId.ToString());

            return RedirectToPage("./Index");
        }

    }
}

