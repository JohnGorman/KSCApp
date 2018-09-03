using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using KSCApp.Models;
using KSCApp.ViewModels;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace KSCApp.Pages
{
    public class ScheduleModel : PageModel
    {
        [BindProperty]
        public ScheduleSelectVM SSVM { get; set; }


        private readonly KSCApp.Data.ApplicationDbContext _context;

        public ScheduleModel(KSCApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<ScheduleVM> ScheduleList { get; set; }
        public List<DateTime> DateList;
        public List<MatchSlot> AvailableSlots;


        public async Task OnGetAsync()
        {
            ViewData["LeagueId"] = new SelectList(_context.League.Where(l=>l.Active == true), "LeagueId", "LeagueName");

            string tempLeagueString = HttpContext.Session.GetString("SelectedLeague");
            string tempSelectDateString = HttpContext.Session.GetString("SelectedDate");

            SSVM = new ScheduleSelectVM();

            SSVM.CurrentLeagueId = 0;

            League SelectedLeague = new League();

            if (tempLeagueString != null)
            {
                SSVM.CurrentLeagueId = Convert.ToInt32(tempLeagueString);
                SelectedLeague = _context.League.FirstOrDefault(l => l.LeagueId == SSVM.CurrentLeagueId);
            }

            if (SelectedLeague.LeagueId<1)
                SelectedLeague = _context.League.OrderByDescending(l => l.LeagueId).FirstOrDefault(l=>l.Active==true);

            SSVM.CurrentLeagueId = SelectedLeague.LeagueId;


            DateTime Today = DateTime.Today;

            DateList = new List<DateTime>();

            DateList = _context.MatchSlot.Where(ms => ms.BookingSlot.Date >= Today)
                .Select(ms => ms.BookingSlot).ToList();


            DateTime compareDate = DateList.Where(d => d.Date == Convert.ToDateTime(tempSelectDateString).Date).FirstOrDefault();


            if (compareDate < Today)
                compareDate = Today;

            SSVM.SelectedDate = compareDate;

            ScheduleList = await _context.MatchSlot.Where(ms => ms.BookingSlot.Date == compareDate.Date && ms.Match.Played != true)
                .Include(ms => ms.Match)
                .Include(ms => ms.Match.Fixture)
                .Include(ms => ms.Match.Fixture.League)
                .Include(ms => ms.Match.Fixture.TeamA)
                .Include(ms => ms.Match.Fixture.TeamB)
                .Include(ms => ms.Match.PlayerA)
                .Include(ms => ms.Match.PlayerB)
                .Select(ms => new ScheduleVM
                {
                    MatchSlotId = ms.MatchSlotId,
                    MatchId = ms.MatchId ?? 0,
                    MatchDetails = string.Format("(L{0}) {1} v {2}", ms.Match.Level, ms.Match.PlayerA.PlayerName, ms.Match.PlayerB.PlayerName),
                    FixtureDetails = string.Format("Team No.{0} {1} v Team No.{2} {3}", ms.Match.Fixture.TeamA.TeamNo, ms.Match.Fixture.TeamA.TeamName
                        , ms.Match.Fixture.TeamB.TeamNo, ms.Match.Fixture.TeamB.TeamName),
                    MatchStart = ms.BookingSlot,
                    LeagueId = ms.Match.Fixture.LeagueId
                })
                .OrderBy(s => s.MatchStart).Where(m => m.LeagueId == SSVM.CurrentLeagueId)
                .AsNoTracking()
                .ToListAsync();

            AvailableSlots = await _context.MatchSlot.Where(ms => ms.BookingSlot.Date == compareDate.Date && ms.MatchId == null && ms.BookingSlot.ToUniversalTime() > DateTime.Now.ToUniversalTime())
                .OrderBy(ms=>ms.BookingSlot)
                .AsNoTracking().
                ToListAsync();

        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return RedirectToPage("./Schedule");
            }

            if (SSVM.CurrentLeagueId!=null)
                HttpContext.Session.SetString("SelectedLeague", SSVM.CurrentLeagueId.ToString());


            if (SSVM.SelectedDate != null)
                HttpContext.Session.SetString("SelectedDate", SSVM.SelectedDate.ToShortDateString());

            return RedirectToPage("./Schedule");
        }
    }
}
