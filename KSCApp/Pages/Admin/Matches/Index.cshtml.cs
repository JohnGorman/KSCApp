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
    public class IndexModel : BasePageModel
    {

        public IndexModel(KSCApp.Data.ApplicationDbContext context) :base(context)
        {
        }

        public IList<Match> Match { get;set; }

        public async Task OnGetAsync()
        {
            SetCurrentLeague();

            Match = await _context.Match.Where(m=>m.Fixture.LeagueId == LeagueSelectVM.SelectedLeague.LeagueId)
                .Include(m => m.Fixture)
                .Include(m => m.PlayerA)
                .Include(m => m.PlayerB).ToListAsync();
        }

    }
}
