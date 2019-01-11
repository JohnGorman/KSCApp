using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using KSCApp.Models;
using KSCApp.ViewModels;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace KSCApp.Pages
{
    public class FixtureModel : BasePageModel
    {
        public IList<Fixture> Fixture { get; set; }

        public FixtureModel(KSCApp.Data.ApplicationDbContext context) : base(context)
        {
        }

        public async Task OnGetAsync()
        {
            SetCurrentLeague();

            Fixture = await _context.Fixture.Where(f => f.LeagueId == LeagueSelectVM.SelectedLeague.LeagueId)
                .Include(f => f.League)
                .Include(f => f.TeamA)
                .Include(f => f.TeamB)
                .OrderBy(f=>f.PlayDate)
                .ToListAsync();

        }
    }
}
