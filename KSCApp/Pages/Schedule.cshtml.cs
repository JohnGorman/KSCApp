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
    public class ScheduleModel : BasePageModel
    {
        public IList<ScheduleVM> ScheduleList { get; set; }
        public List<MatchSlot> AvailableSlots;

        public ScheduleModel(KSCApp.Data.ApplicationDbContext context) : base(context)
        {
        }

        public async Task OnGetAsync()
        {
            SetCurrentLeague();

            SetSelectedDate();

            var DateList = _context.MatchSlot.Where(ms => ms.BookingSlot.Date >= DateTime.Today)
                .Select(ms => ms.BookingSlot).ToList();

            ScheduleList = await _context.MatchSlot.Where(ms => ms.BookingSlot.Date == LeagueSelectVM.SelectedDate.Date && ms.Match.Played != true)
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
                .OrderBy(s => s.MatchStart).Where(m => m.LeagueId == LeagueSelectVM.SelectedLeague.LeagueId)
                .AsNoTracking()
                .ToListAsync();

            AvailableSlots = await _context.MatchSlot.Where(ms => ms.BookingSlot.Date == LeagueSelectVM.SelectedDate.Date.Date && ms.MatchId == null && ms.BookingSlot.ToUniversalTime() > DateTime.Now.ToUniversalTime())
                .OrderBy(ms=>ms.BookingSlot)
                .AsNoTracking().
                ToListAsync();

        }

        public override IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            SetSelectedLeagueSession();

            SetSelectedDateSession();

            return RedirectToPage();

        }
    }
}
