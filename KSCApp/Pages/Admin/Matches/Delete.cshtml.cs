using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KSCApp.Data;
using KSCApp.Models;

namespace KSCApp.Pages.Admin.Matches
{
    public class DeleteModel : PageModel
    {
        private readonly KSCApp.Data.ApplicationDbContext _context;

        public DeleteModel(KSCApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Match Match { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Match = await _context.Match
                .Include(m => m.Fixture)
                .Include(m => m.PlayerA)
                .Include(m => m.PlayerB).FirstOrDefaultAsync(m => m.MatchId == id);

            if (Match == null)
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

            Match = await _context.Match.FindAsync(id);

            if (Match != null)
            {
                _context.Match.Remove(Match);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
