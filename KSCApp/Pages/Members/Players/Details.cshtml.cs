using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KSCApp.Data;
using KSCApp.Models;
using KSCApp.ViewModels;

namespace KSCApp.Pages.Members.Players
{
    public class DetailsModel : PageModel
    {
        private readonly KSCApp.Data.ApplicationDbContext _context;

        public DetailsModel(KSCApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public PlayerVM PlayerVM { get; set; }
        public IList<ScheduleVM> ScheduleList { get; set; }
        public List<DateTime> DateList;
        public List<MatchSlot> OverDueMatches;
        public IQueryable<Match> CancelledMatches;
        public IList<MatchResultVM> ResultList { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Player = await _context.Player
            //    .Include(p=>p.KSCAppUser)
            //    .FirstOrDefaultAsync(m => m.PlayerId == id);

            PlayerVM = await (from player in _context.Player
                                                join user in _context.Users on player.UserId equals user.Id into pvm
                                                from con in pvm.DefaultIfEmpty()
                                                select new PlayerVM
                                                {
                                                    Id = player.PlayerId,
                                                    PlayerName = player.PlayerName,
                                                    PlayerStatus = player.PlayerStatus,
                                                    PlayingLeague = player.PlayingLeague,
                                                    ProfilePicture = player.ProfilePicture,
                                                    ContactNo = player.ContactNo,
                                                    PlayerType = player.PlayerType,
                                                    Rank = player.Rank,
                                                    Email = con.Email
                                                })
                                                .FirstOrDefaultAsync(m => m.Id == id); ;


            if (PlayerVM == null)
            {
                return NotFound();
            }


            ScheduleList = await _context.MatchSlot.Where(ms => ms.BookingSlot.Date >= DateTime.Today && ms.Match.Played != true && (ms.Match.PlayerAId == id || ms.Match.PlayerBId == id))
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
                    FixtureDetails = string.Format("{0}",ms.Match.Fixture.League.LeagueName),
                    MatchStartDateTime = ms.BookingSlot,
                    LeagueId = ms.Match.Fixture.LeagueId
                })
                .OrderBy(s => s.MatchStart)
                .AsNoTracking()
                .ToListAsync();

            //List a of all MatchIds in MatchSlots
            var a = _context.MatchSlot.Where(ms => ms.MatchId != null)
                .Select(m => new
                {
                    id = (int)m.MatchId
                }).ToList();


            //List of all Matches where MatchId NOT in list a
            CancelledMatches = _context.Match.Where(m1 => !a.Any(m2 => m2.id == m1.MatchId) && m1.Played == false && (m1.PlayerAId == id || m1.PlayerBId == id))
                .Include(m => m.PlayerA)
                .Include(m => m.PlayerB)
                .Include(m => m.Fixture.League);


            //List of all Matches that are overdue
            OverDueMatches = await _context.MatchSlot.Where(m => m.Match.Played == false && m.BookingSlot < DateTime.Today && (m.Match.PlayerAId == id || m.Match.PlayerBId == id))
                .Include(m => m.Match.PlayerA)
                .Include(m => m.Match.PlayerB)
                .Include(m => m.Match.Fixture.League)
                .Where(m => m.Match.PlayerAId == id || m.Match.PlayerBId == id).ToListAsync();

            //List of all Results for this Player
            ResultList = await _context.Match.Where(m => m.PlayedDate != null && (m.PlayerAId == id || m.PlayerBId == id))
                    .Include(m => m.Fixture)
                    .Include(m => m.Fixture.TeamA)
                    .Include(m => m.Fixture.TeamB)
                    .Include(m => m.PlayerA)
                    .Include(m => m.PlayerB)
                    .Include(m => m.GameResults)
                    .Select(u => new MatchResultVM
                    {
                        FixtureDetails = u.Fixture.TeamA.TeamName + " v " + u.Fixture.TeamB.TeamName,
                        MatchDetails = String.Format("{0} ({1}) v {2} ({3})", u.PlayerA.PlayerName, u.PlayerAgames, u.PlayerB.PlayerName, u.PlayerBgames),
                        GameResults = "",
                        MatchId = u.MatchId,
                        DatePlayed = (u.PlayedDate ?? DateTime.Now).ToString("dd/MM/yyy")
                    })
                    .OrderByDescending(u => u.DatePlayed)
                    .AsNoTracking()
                    .ToListAsync();

            foreach (var res in ResultList)
            {
                string gameResults = "(";
                var games = _context.GameResult.Where(g => g.MatchId == res.MatchId);
                foreach (var game in games)
                {
                    gameResults += string.Format(" {0}-{1},", game.PlayerApoints, game.PlayerBpoints);
                }
                gameResults = gameResults.Remove(gameResults.Length - 1);  //Get rid of trailing ,
                gameResults += " )";
                res.GameResults = gameResults;
            }

            return Page();
        }
    }
}
