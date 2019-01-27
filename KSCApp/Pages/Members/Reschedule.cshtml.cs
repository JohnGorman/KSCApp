using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using KSCApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;

namespace KSCApp.Pages.Members
{
    public class RescheduleModel : BasePageModel
    {
        public List<Match> OverDueMatches { get; set; }
        public IQueryable<Match> CancelledMatches { get; set; }

        public RescheduleModel(KSCApp.Data.ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            SetCurrentLeague();

            if (id!=null)
            {
                var MatchSlot = _context.MatchSlot.FirstOrDefault(m => m.MatchSlotId == id);

                try
                {
                    MatchSlot.MatchId = null;
                    _context.MatchSlot.Update(MatchSlot);
                    await _context.SaveChangesAsync();
                }
                catch
                {
                    throw;
                }
            }

            //List A of all MatchIds in MatchSlots
            var a = _context.MatchSlot.Where(ms => ms.MatchId != null)
                .Select(m => new
                {
                    id = (int)m.MatchId
                }).ToList();


            //List of all Matches where MatchId NOT in list A
            CancelledMatches = _context.Match.Where(m1 => !a.Any(m2 => m2.id == m1.MatchId) && m1.Played==false  && m1.Fixture.LeagueId == LeagueSelectVM.SelectedLeague.LeagueId)
                .Include(m=>m.PlayerA)
                .Include(m=>m.PlayerB)
                .Include(m=>m.Fixture.League);


            //List of all Matches that are overdue
            OverDueMatches = await _context.MatchSlot.Where(m => m.Match.Played == false && m.BookingSlot < DateTime.Today)
                .Select(m=>m.Match)
                .Include(m => m.PlayerA)
                .Include(m => m.PlayerB)
                .Include(m => m.Fixture.League)
                .Where(m=>m.Fixture.LeagueId == LeagueSelectVM.SelectedLeague.LeagueId).Distinct().ToListAsync();

            return Page();
        }

    }
}