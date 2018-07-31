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

namespace KSCApp.Pages.Admin.MatchSlots
{
    public class EditModel : PageModel
    {
        private readonly KSCApp.Data.ApplicationDbContext _context;

        public EditModel(KSCApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public MatchSlot MatchSlot { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MatchSlot = await _context.MatchSlot
                .Include(m => m.Match).FirstOrDefaultAsync(m => m.MatchSlotId == id);

            if (MatchSlot == null)
            {
                return NotFound();
            }
           ViewData["MatchId"] = new SelectList(_context.Match, "MatchId", "MatchId");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(MatchSlot).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MatchSlotExists(MatchSlot.MatchSlotId))
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

        private bool MatchSlotExists(int id)
        {
            return _context.MatchSlot.Any(e => e.MatchSlotId == id);
        }
    }
}
