using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
                MatchSlot = _context.MatchSlot.Where(ms => ms.MatchSlotId == id).FirstOrDefault();

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