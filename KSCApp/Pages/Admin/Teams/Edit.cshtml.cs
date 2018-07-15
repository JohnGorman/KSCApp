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

namespace KSCApp.Pages.Admin.Teams
{
    public class EditModel : PageModel
    {
        private readonly KSCApp.Data.ApplicationDbContext _context;

        public EditModel(KSCApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Team Team { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Team = await _context.Team
                .Include(t => t.League).FirstOrDefaultAsync(m => m.TeamId == id);

            if (Team == null)
            {
                return NotFound();
            }
           ViewData["LeagueId"] = new SelectList(_context.League, "LeagueId", "LeagueId");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Team).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeamExists(Team.TeamId))
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

        private bool TeamExists(int id)
        {
            return _context.Team.Any(e => e.TeamId == id);
        }
    }
}
