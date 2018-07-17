using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using KSCApp.Data;
using KSCApp.Models;

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
        ViewData["TeamAId"] = new SelectList(_context.Team, "TeamId", "TeamName");
        ViewData["TeamBId"] = new SelectList(_context.Team, "TeamId", "TeamName");
            return Page();
        }

        [BindProperty]
        public Fixture Fixture { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Fixture.Add(Fixture);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}