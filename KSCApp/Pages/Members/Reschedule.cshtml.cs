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
    public class RescheduleModel : PageModel
    {
        private readonly KSCApp.Data.ApplicationDbContext _context;

        public RescheduleModel(KSCApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        //public IQueryable<Match> OverDueMatches { get; set; }
        public List<MatchSlot> OverDueMatches;
        public IQueryable<Match> CancelledMatches;

        [BindProperty]
        public League SelectedLeague { get; set; }


        public async Task<IActionResult> OnGetAsync(int? id)
        {
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

            //Get a list of active leagues for the drop down league selector
            ViewData["LeagueId"] = new SelectList(_context.League.Where(l => l.Active == true), "LeagueId", "LeagueName");

            //Check for a session cookie selected league
            string tempLeagueString = HttpContext.Session.GetString("SelectedLeague");

            int CurrentLeagueId = 0;

            //If there is a previously selected league in this session load this one
            if (tempLeagueString != null)
            {
                CurrentLeagueId = Convert.ToInt32(tempLeagueString);
                SelectedLeague = _context.League.FirstOrDefault(l => l.LeagueId == CurrentLeagueId && l.Active == true);
            }

            //If no session selected league, select the latest league as default
            if (SelectedLeague == null)
                SelectedLeague = _context.League.OrderByDescending(l => l.LeagueId).FirstOrDefault(l => l.Active == true);

            CurrentLeagueId = SelectedLeague.LeagueId;

            //List a of all MatchIds in MatchSlots
            var a = _context.MatchSlot.Where(ms => ms.MatchId != null)
                .Select(m => new
                {
                    id = (int)m.MatchId
                }).ToList();


            //List of all Matches where MatchId NOT in list a
            CancelledMatches = _context.Match.Where(m1 => !a.Any(m2 => m2.id == m1.MatchId) && m1.Played==false  && m1.Fixture.LeagueId == CurrentLeagueId)
                .Include(m=>m.PlayerA)
                .Include(m=>m.PlayerB)
                .Include(m=>m.Fixture.League);


            //List of all Matches that are overdue
            OverDueMatches = await _context.MatchSlot.Where(m => m.Match.Played == false && m.BookingSlot < DateTime.Today)
                .Include(m => m.Match.PlayerA)
                .Include(m => m.Match.PlayerB)
                .Include(m => m.Match.Fixture.League)
                .Where(m=>m.Match.Fixture.LeagueId == CurrentLeagueId).ToListAsync();

            return Page();
        }

    }
}