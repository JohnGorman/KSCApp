using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KSCApp.Models;

namespace KSCApp.Pages.Members
{
    public class SlotToMatchModel : PageModel
    {
        private readonly KSCApp.Data.ApplicationDbContext _context;

        public SlotToMatchModel(KSCApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public MatchSlot MatchSlot { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            try
            {
                MatchSlot = await _context.MatchSlot.FirstOrDefaultAsync(ms => ms.MatchSlotId == id);

                //ViewData[""]
            }
            catch
            {
                throw;
            }

            return Page();
        }
    }
}