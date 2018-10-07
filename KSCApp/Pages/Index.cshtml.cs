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
using Microsoft.EntityFrameworkCore;

namespace KSCApp.Pages
{
    public class IndexModel : PageModel
    {
        public IActionResult OnGet()
        {
            //Get a list of active leagues for the drop down league selector
            ViewData["LeagueId"] = new SelectList(_context.League.Where(l=>l.Active==true), "LeagueId", "LeagueName");

            //Check for a session cookie selected league
            string tempLeagueString = HttpContext.Session.GetString("SelectedLeague");

            int CurrentLeagueId = 0;

            //If there is a previously selected league in this session load this one
            if (tempLeagueString != null)
            {
                CurrentLeagueId = Convert.ToInt32(tempLeagueString);
                SelectedLeague = _context.League.FirstOrDefault(l => l.LeagueId == CurrentLeagueId && l.Active == true);
            }

            //If no session selected league, select the latest league as default
            if (SelectedLeague == null)
                SelectedLeague = _context.League.OrderByDescending(l => l.LeagueId).FirstOrDefault(l=>l.Active == true);


            if (SelectedLeague == null)
                return Page();


            CurrentLeagueId = SelectedLeague.LeagueId;


            LeagueTableA = (from Team in _context.Team
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
                         ).Where(c=>c.LeagueId == CurrentLeagueId && c.Section == "A")
                         .OrderByDescending(c=>c.GamesWon)
                         .ToList();

            LeagueTableB = (from Team in _context.Team
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
                            }).GroupBy(t => t.TeamId)
             .Select(ct => new TeamVM
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
             ).Where(c => c.LeagueId == CurrentLeagueId && c.Section == "B")
             .OrderByDescending(c => c.GamesWon)
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

        public IList<TeamVM> LeagueTableA { get; set; }
        public IList<TeamVM> LeagueTableB { get; set; }


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

