using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using KSCApp.Models;
using KSCApp.ViewModels;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace KSCApp.Pages
{
    public class FixtureModel : PageModel
    {

        [BindProperty]
        public League SelectedLeague { get; set; }


        private readonly KSCApp.Data.ApplicationDbContext _context;

        public FixtureModel(KSCApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Fixture> Fixture { get; set; }


        public async Task OnGetAsync()
        {
            ViewData["LeagueId"] = new SelectList(_context.League.Where(l=>l.Active == true), "LeagueId", "LeagueName");

            string tempLeagueString = HttpContext.Session.GetString("SelectedLeague");

            int CurrentLeagueId = 0;

            if (tempLeagueString != null)
            {
                CurrentLeagueId = Convert.ToInt32(tempLeagueString);
                SelectedLeague = _context.League.FirstOrDefault(l => l.LeagueId == CurrentLeagueId && l.Active == true);
            }

            if (SelectedLeague == null)
                SelectedLeague = _context.League.OrderByDescending(l => l.LeagueId).FirstOrDefault(l=>l.Active == true);

            CurrentLeagueId = SelectedLeague.LeagueId;

            Fixture = await _context.Fixture.Where(f => f.LeagueId == CurrentLeagueId)
                .Include(f => f.League)
                .Include(f => f.TeamA)
                .Include(f => f.TeamB)
                .OrderBy(f=>f.PlayDate)
                .ToListAsync();

        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }


            HttpContext.Session.SetString("SelectedLeague", SelectedLeague.LeagueId.ToString());

            return RedirectToPage("./Fixtures");
        }
    }
}
