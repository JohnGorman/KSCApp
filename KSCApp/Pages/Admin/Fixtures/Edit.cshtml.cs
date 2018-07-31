using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KSCApp.Data;
using KSCApp.Models;

namespace KSCApp.Pages.Admin.Fixtures
{
    public class EditModel : PageModel
    {
        private readonly KSCApp.Data.ApplicationDbContext _context;

        public EditModel(KSCApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Fixture Fixture { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Fixture = await _context.Fixture
                .Include(f => f.League)
                .Include(f => f.TeamA)
                .Include(f => f.TeamB).FirstOrDefaultAsync(m => m.FixtureId == id);

            if (Fixture == null)
            {
                return NotFound();
            }
           ViewData["LeagueId"] = new SelectList(_context.League, "LeagueId", "LeagueName");
           ViewData["TeamAId"] = new SelectList(_context.Team, "TeamId", "TeamName");
           ViewData["TeamBId"] = new SelectList(_context.Team, "TeamId", "TeamName");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Fixture).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FixtureExists(Fixture.FixtureId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool FixtureExists(int id)
        {
            return _context.Fixture.Any(e => e.FixtureId == id);
        }
    }
}
