using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KSCApp.Data;
using KSCApp.Models;

namespace KSCApp.Pages.Admin.TeamPlayers
{
    public class DeleteModel : PageModel
    {
        private readonly KSCApp.Data.ApplicationDbContext _context;

        public DeleteModel(KSCApp.Data.ApplicationDbContext context)
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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TeamPlayer = await _context.TeamPlayer.FindAsync(id);

            if (TeamPlayer != null)
            {
                _context.TeamPlayer.Remove(TeamPlayer);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
