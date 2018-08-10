using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using KSCApp.Models;
using KSCApp.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KSCApp.Pages.Members
{
    public class MatchToSlotModel : PageModel
    {
        private readonly KSCApp.Data.ApplicationDbContext _context;

        public MatchToSlotModel(KSCApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        //[BindProperty]
        //public Match Match { get; set; }

        [BindProperty]
        public MatchSlotSelectVM MatchSlotSelectVM { get; set; }  

        public IList<MatchSlot> AvailableSlots { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            try
            {
                AvailableSlots = await _context.MatchSlot.Where(m => m.MatchId == null && m.BookingSlot > DateTime.Now)
                    .OrderBy(m=>m.BookingSlot)
                    .ToListAsync();

                MatchSlotSelectVM = await _context.Match.Where(ms => ms.MatchId == id)
                    .Select(m=> new MatchSlotSelectVM{
                        SelectedMatchId = m.MatchId,
                        LeagueName = m.Fixture.League.LeagueName,
                        MatchDetails = String.Format("L{0} {1} v {2}", m.Level, m.PlayerA.PlayerName, m.PlayerB.PlayerName)
                    }).FirstOrDefaultAsync();

                

                //MatchSlotSelectVM = Match.Select()

                //MatchSlotSelectVM = AvailableSlots.OrderBy(ms => ms.BookingSlot).FirstOrDefault();

                ViewData["MatchSlotId"] = new SelectList(AvailableSlots, "MatchSlotId", "BookingSlot");
            }
            catch
            {
                throw;
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            int tempMatchId = MatchSlotSelectVM.SelectedMatchId;

            MatchSlot matchSlot = _context.MatchSlot.FirstOrDefault(ms => ms.MatchSlotId == MatchSlotSelectVM.SelectedMatchSlotId);

            if (matchSlot!=null)
            {
                matchSlot.MatchId = MatchSlotSelectVM.SelectedMatchId;

                try
                {
                    _context.MatchSlot.Update(matchSlot);
                    await _context.SaveChangesAsync();
                }
                catch
                {
                    throw;
                }

            }

            return RedirectToPage("./Reschedule");
        }


    }
}