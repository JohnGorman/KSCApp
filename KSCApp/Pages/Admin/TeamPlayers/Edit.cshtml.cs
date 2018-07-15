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

namespace KSCApp.Pages.Admin.TeamPlayers
{
    public class EditModel : PageModel
    {
        private readonly KSCApp.Data.ApplicationDbContext _context;

        public EditModel(KSCApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public TeamPlayer TeamPlayer { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TeamPlayer = await _context.TeamPlayer
                .Include(t => t.Player)
                .Include(t => t.Team).FirstOrDefaultAsync(m => m.TeamPlayerId == id);

            if (TeamPlayer == null)
            {
                return NotFound();
            }
           ViewData["PlayerId"] = new SelectList(_context.Player, "PlayerId", "PlayerId");
           ViewData["TeamId"] = new SelectList(_context.Team, "TeamId", "TeamId");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(TeamPlayer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeamPlayerExists(TeamPlayer.TeamPlayerId))
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

        private bool TeamPlayerExists(int id)
        {
            return _context.TeamPlayer.Any(e => e.TeamPlayerId == id);
        }
    }
}
