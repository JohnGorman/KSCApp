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
    public class DetailsModel : PageModel
    {
        private readonly KSCApp.Data.ApplicationDbContext _context;

        public DetailsModel(KSCApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

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
    }
}
