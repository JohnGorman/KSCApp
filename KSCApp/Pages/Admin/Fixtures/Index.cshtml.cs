using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KSCApp.Data;
using KSCApp.Models;

namespace KSCApp.Pages.Admin.Fixtures
{
    public class IndexModel : PageModel
    {
        private readonly KSCApp.Data.ApplicationDbContext _context;

        public IndexModel(KSCApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Fixture> Fixture { get;set; }

        public async Task OnGetAsync()
        {
            Fixture = await _context.Fixture
                .Include(f => f.League)
                .Include(f => f.TeamA)
                .Include(f => f.TeamB).ToListAsync();
        }
    }
}
