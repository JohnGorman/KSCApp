using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using KSCApp.Data;
using KSCApp.Models;

namespace KSCApp.Pages.Admin.TeamPlayers
{
    public class CreateModel : PageModel
    {
        private readonly KSCApp.Data.ApplicationDbContext _context;

        public CreateModel(KSCApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["PlayerId"] = new SelectList(_context.Player, "PlayerId", "PlayerId");
        ViewData["TeamId"] = new SelectList(_context.Team, "TeamId", "TeamId");
            return Page();
        }

        [BindProperty]
        public TeamPlayer TeamPlayer { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.TeamPlayer.Add(TeamPlayer);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}