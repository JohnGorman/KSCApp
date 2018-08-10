using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using KSCApp.Models;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;



namespace KSCApp.Pages.Shared
{
    public class _LeagueSelectModel : PageModel
    {
        public void OnGet()
        {
            ViewData["LeagueId"] = new SelectList(_context.League, "LeagueId", "LeagueName");

            string tempLeagueString = HttpContext.Session.GetString("SelectedLeague");

            int CurrentLeagueId = 0;

            if (tempLeagueString != null)
            {
                CurrentLeagueId = Convert.ToInt32(tempLeagueString);
                SelectedLeague = _context.League.FirstOrDefault(l => l.LeagueId == CurrentLeagueId);
            }

            if (SelectedLeague == null)
                SelectedLeague = _context.League.OrderByDescending(l => l.LeagueId).First();
        }

        [BindProperty]
        public League SelectedLeague { get; set; }


        private readonly KSCApp.Data.ApplicationDbContext _context;

        public _LeagueSelectModel(KSCApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }


            HttpContext.Session.SetString("SelectedLeague", SelectedLeague.LeagueId.ToString());

            return Page();
        }


    }
}