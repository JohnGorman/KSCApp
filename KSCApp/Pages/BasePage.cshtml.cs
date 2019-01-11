using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KSCApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using KSCApp.Models;
using KSCApp.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace KSCApp.Pages
{
    public class BasePageModel : PageModel
    {
        public readonly ApplicationDbContext _context;

        [BindProperty]
        public LeagueSelectVM LeagueSelectVM { get; set; }

        public BasePageModel(ApplicationDbContext context)
        {
            _context = context;

            LeagueSelectVM = new LeagueSelectVM();

            LeagueSelectVM.LeagueSelectList = _context.League.Where(l=>l.Active == true)
                .OrderByDescending(l => l.LeagueId)
                .Select(l => new SelectListItem
                {
                    Value = l.LeagueId.ToString(),
                    Text = l.LeagueName
                })
                .ToList();


            //Set default return page
            LeagueSelectVM.RedirectPage = "./Index";
        }


        public void SetCurrentLeague()
        {
            //Check for a session cookie selected league
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("SelectedLeague")))
            {
                LeagueSelectVM.SelectedLeague = _context.League.Where(l=>l.Active==true)
                    .OrderByDescending(l=>l.LeagueId)
                    .FirstOrDefault();
                LeagueSelectVM.SelectedLeagueId = LeagueSelectVM.SelectedLeague.LeagueId.ToString();
            }
            else
            {
                LeagueSelectVM.SelectedLeagueId = HttpContext.Session.GetString("SelectedLeague");
                LeagueSelectVM.SelectedLeague = _context.League.FirstOrDefault(l => l.LeagueId == Convert.ToInt32(LeagueSelectVM.SelectedLeagueId));
            }
        }

        public void SetSelectedDate()
        {
            //Check for a session cookie selected date
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("SelectedDate")))
            {
                LeagueSelectVM.SelectedDate = DateTime.Today;
            }
            else
            {
                LeagueSelectVM.SelectedDate = Convert.ToDateTime(HttpContext.Session.GetString("SelectedDate")).Date;
            }
        }

        //public virtual async Task OnGetAsync()
        //{
        //    SetCurrentLeague();

        //    var model = await _context.Team.FirstOrDefaultAsync(l => l.LeagueId == Convert.ToInt32(LeagueSelectVM.SelectedLeagueId));

        //    LeagueSelectVM.RedirectPage = "./Index";
        //}

        public virtual IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            HttpContext.Session.SetString("SelectedLeague", LeagueSelectVM.SelectedLeagueId);
            HttpContext.Session.SetString("SelectedDate", LeagueSelectVM.SelectedDate.ToShortDateString());

            return RedirectToPage(LeagueSelectVM.RedirectPage);
        }

    }
}