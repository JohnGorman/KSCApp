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
    public class IndexModel : PageModel
    {
        private readonly KSCApp.Data.ApplicationDbContext _context;

        public IndexModel(KSCApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<League> League { get;set; }

        public async Task OnGetAsync()
        {
            League = await _context.League.ToListAsync();
        }
    }
}
