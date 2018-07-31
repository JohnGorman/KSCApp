using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KSCApp.Data;
using KSCApp.Models;

namespace KSCApp.Pages.Admin.MatchSlots
{
    public class DetailsModel : PageModel
    {
        private readonly KSCApp.Data.ApplicationDbContext _context;

        public DetailsModel(KSCApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

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
            return Page();
        }
    }
}
