using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using KSCApp.Data;
using KSCApp.Models;

namespace KSCApp.Pages.Admin.Leagues
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
            return Page();
        }

        [BindProperty]
        public League League { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.League.Add(League);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}