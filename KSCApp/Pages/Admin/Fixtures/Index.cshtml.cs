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

namespace KSCApp.Pages.Admin.Fixtures
{
    public class IndexModel : PageModel
    {
        private readonly KSCApp.Data.ApplicationDbContext _context;

        public IndexModel(KSCApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public League SelectedLeague { get; set; }

        public IList<Fixture> Fixture { get;set; }

        public async Task OnGetAsync()
        {
            ViewData["LeagueId"] = new SelectList(_context.League, "LeagueId", "LeagueName");

            string tempLeagueString = HttpContext.Session.GetString("SelectedLeague");

            int CurrentLeagueId = 1;


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

            Fixture = await _context.Fixture.Where(f=>f.LeagueId == CurrentLeagueId)
                .Include(f => f.League)
                .Include(f => f.TeamA)
                .Include(f => f.TeamB).ToListAsync();
        }


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
