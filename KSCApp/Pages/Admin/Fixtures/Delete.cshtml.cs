using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KSCApp.Data;
using KSCApp.Models;

namespace KSCApp.Pages.Admin.Fixtures
{
    public class DeleteModel : PageModel
    {
        private readonly KSCApp.Data.ApplicationDbContext _context;

        public DeleteModel(KSCApp.Data.ApplicationDbContext context)
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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Fixture = await _context.Fixture.FindAsync(id);

            if (Fixture != null)
            {
                _context.Fixture.Remove(Fixture);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
