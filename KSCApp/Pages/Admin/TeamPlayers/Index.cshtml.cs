using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KSCApp.Data;
using KSCApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using KSCApp.ViewModels;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;

namespace KSCApp.Pages.Admin.TeamPlayers
{
    public class IndexModel : PageModel
    {
        private readonly KSCApp.Data.ApplicationDbContext _context;

        public IndexModel(KSCApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<TeamPlayer> TeamPlayer { get;set; }

        [BindProperty]
        public League SelectedLeague { get; set; }
        [BindProperty]
        public Team SelectedTeam { get; set; }


        public async Task OnGetAsync()
        {
           
            string tempLeagueString = HttpContext.Session.GetString("SelectedLeague");
            string tempTeamString = HttpContext.Session.GetString("SelectedTeam");

            int CurrentLeagueId = 1;
            int CurrentTeamId = 1;


            if (tempLeagueString == null)
            {
                SelectedLeague = _context.League.OrderByDescending(c => c.LeagueId).FirstOrDefault();
                CurrentLeagueId = SelectedLeague.LeagueId;
            }
            else
            {
                CurrentLeagueId = Convert.ToInt32(tempLeagueString);
                CurrentTeamId = Convert.ToInt32(tempTeamString);
            }

            SelectedLeague = _context.League.FirstOrDefault(c => c.LeagueId == CurrentLeagueId);

            if (SelectedLeague == null)
                SelectedLeague = _context.League.First();

            CurrentLeagueId = SelectedLeague.LeagueId;

            var teamsList = _context.Team.OrderBy(t=>t.TeamId).Where(t => t.LeagueId == CurrentLeagueId).ToList();

            SelectedTeam = teamsList.FirstOrDefault(t => t.TeamId == CurrentTeamId);

            if (SelectedTeam == null)
                SelectedTeam = teamsList.First();


            CurrentTeamId = SelectedTeam.TeamId;


            ViewData["LeagueId"] = new SelectList(_context.League, "LeagueId", "LeagueName");
            ViewData["TeamId"] = new SelectList(teamsList, "TeamId", "TeamName");

            TeamPlayer = await _context.TeamPlayer.Where(tp=>tp.TeamId == CurrentTeamId)
                .OrderBy(t=>t.Level)
                .Include(t => t.Player)
                .Include(t => t.Team).ToListAsync();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (SelectedLeague != null)
                HttpContext.Session.SetString("SelectedLeague", SelectedLeague.LeagueId.ToString());

            if (SelectedTeam != null)              
                HttpContext.Session.SetString("SelectedTeam", SelectedTeam.TeamId.ToString());

            return RedirectToPage("./Index");
        }
    }
}
