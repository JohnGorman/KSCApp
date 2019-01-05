using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using KSCApp.Data;
using KSCApp.Models;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using KSCApp.ViewModels;

namespace KSCApp.ViewComponents
{
    public class LeagueSelectViewComponent : ViewComponent
    {
        private readonly KSCApp.Data.ApplicationDbContext _context;

        public LeagueSelectViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }



        //public League SelectedLeague { get; set; }
        //public IEnumerable<SelectListItem> LeagueList { get; set; }
        //public string SelectedLeagueId;

        [BindProperty]
        public LeagueSelectVM LeagueSelectVM { get; set; }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            LeagueSelectVM = new LeagueSelectVM();

            LeagueSelectVM.LeagueSelectList = await _context.League.Where(l => l.Active == true)
                .OrderByDescending(l => l.LeagueId)
                .Select(l => new SelectListItem
                {
                    Value = l.LeagueId.ToString(),
                    Text = l.LeagueName
                })
                .ToListAsync();

            //Check for a session cookie selected league
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("SelectedLeague")))
            {
                LeagueSelectVM.SelectedLeague = await _context.League.Where(l => l.Active == true)
                    .OrderByDescending(l => l.LeagueId)
                    .FirstOrDefaultAsync();
                LeagueSelectVM.SelectedLeagueId = LeagueSelectVM.SelectedLeague.LeagueId.ToString();
            }
            else
            {
                LeagueSelectVM.SelectedLeagueId = HttpContext.Session.GetString("SelectedLeague");
                LeagueSelectVM.SelectedLeague = await _context.League.FirstOrDefaultAsync(l => l.LeagueId == Convert.ToInt32(LeagueSelectVM.SelectedLeagueId));
            }


            return View(LeagueSelectVM);

        }
    }
}
