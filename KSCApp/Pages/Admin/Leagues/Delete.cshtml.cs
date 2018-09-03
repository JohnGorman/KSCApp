using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KSCApp.Data;
using KSCApp.Models;

namespace KSCApp.Pages.Admin.Leagues
{
    public class DeleteModel : PageModel
    {
        private readonly KSCApp.Data.ApplicationDbContext _context;

        public DeleteModel(KSCApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public League League { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            League = await _context.League.FirstOrDefaultAsync(m => m.LeagueId == id);

            if (League == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            League = await _context.League.FindAsync(id);

            if (League != null)
            {
                if (League.Fixtures != null)
                {
                    var fixtures = _context.Fixture.Where(f => f.LeagueId == League.LeagueId);
                    foreach (var fixture in fixtures)
                    {
                        var matches = _context.Match.Where(m => m.FixtureId == fixture.FixtureId);
                        foreach (var match in matches)
                        {
                            var results = _context.GameResult.Where(gr => gr.MatchId == match.MatchId);
                            foreach (var result in results)
                            {
                                _context.GameResult.Remove(result);
                            }
                            var slots = _context.MatchSlot.Where(ms => ms.MatchId == match.MatchId);
                            foreach (var solt in slots)
                            {
                                _context.MatchSlot.Remove(solt);
                            }
                                _context.Match.Remove(match);
                        }
                        _context.Fixture.Remove(fixture);
                    }                    
                }
                _context.League.Remove(League);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch
                {
                    throw;
                }               
            }

            return RedirectToPage("./Index");
        }
    }
}
