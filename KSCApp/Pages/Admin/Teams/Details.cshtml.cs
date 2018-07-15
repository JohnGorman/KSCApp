using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KSCApp.Data;
using KSCApp.Models;

namespace KSCApp.Pages.Admin.Teams
{
    public class DetailsModel : PageModel
    {
        private readonly KSCApp.Data.ApplicationDbContext _context;

        public DetailsModel(KSCApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Team Team { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Team = await _context.Team
                .Include(t => t.League).FirstOrDefaultAsync(m => m.TeamId == id);

            if (Team == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
