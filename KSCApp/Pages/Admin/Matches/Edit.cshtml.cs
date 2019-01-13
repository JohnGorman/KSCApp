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

namespace KSCApp.Pages.Admin.Matches
{
    public class EditModel : PageModel
    {
        private readonly KSCApp.Data.ApplicationDbContext _context;

        //TeamPlayer TeamPlayerA { get; set; }
        //TeamPlayer TeamPlayerB { get; set; }

        public EditModel(KSCApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Match Match { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Match = await _context.Match
                .Include(m => m.Fixture)
                .Include(m => m.PlayerA)
                .Include(m => m.PlayerB).FirstOrDefaultAsync(m => m.MatchId == id);

            if (Match == null)
            {
                return NotFound();
            }
           ViewData["FixtureId"] = new SelectList(_context.Fixture, "FixtureId", "FixtureId");
           ViewData["PlayerAId"] = new SelectList(_context.Player.OrderBy(p=>p.PlayerName), "PlayerId", "PlayerName");
           ViewData["PlayerBId"] = new SelectList(_context.Player.OrderBy(p => p.PlayerName), "PlayerId", "PlayerName");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Match).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MatchExists(Match.MatchId))
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

        public async Task<IActionResult> OnPostResetMatchAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var gameResults = await _context.GameResult.Where(M => M.MatchId == Match.MatchId).ToListAsync();

            var fixture = await _context.Fixture.FirstOrDefaultAsync(f => f.FixtureId == Match.FixtureId);

            var teamPlayerA = await _context.TeamPlayer.Where(t => t.PlayerId == Match.PlayerAId)
                .Include(t => t.Team).FirstOrDefaultAsync(t => t.Team.LeagueId == fixture.LeagueId);

            var teamPlayerB = await _context.TeamPlayer.Where(tp => tp.PlayerId == Match.PlayerBId)
                .Include(t => t.Team).FirstOrDefaultAsync(t=>t.Team.LeagueId == fixture.LeagueId);


            if (Match.PlayerAgames > Match.PlayerBgames)
            {
                teamPlayerA.MatchesWon -= 1;
            }
            else
            {
                teamPlayerB.MatchesWon -= 1;
            }

            teamPlayerA.MatchesPlayed -= 1;
            teamPlayerA.GamesWon -= Match.PlayerAgames;
            teamPlayerA.GamesLost -= Match.PlayerBgames;

            teamPlayerB.MatchesPlayed -= 1;
            teamPlayerB.GamesWon -= Match.PlayerBgames;
            teamPlayerB.GamesLost -= Match.PlayerAgames;

            Match.Played = false;
            Match.PlayedDate = null;
            Match.PlayerAgames = 0;
            Match.PlayerBgames = 0;

            try
            {
                foreach (GameResult game in gameResults)
                {
                    _context.GameResult.Remove(game);
                }

                _context.TeamPlayer.Update(teamPlayerA);
                _context.TeamPlayer.Update(teamPlayerB);
                _context.Match.Update(Match);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MatchExists(Match.MatchId))
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

        private bool MatchExists(int id)
        {
            return _context.Match.Any(e => e.MatchId == id);
        }

    }
}
