using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KSCApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KSCApp.ViewModels
{
    public class LeagueSelectVM
    {
        public League SelectedLeague { get; set; }
        public string SelectedLeagueId { get; set; }
        public IEnumerable<SelectListItem> LeagueSelectList { get; set; }
        public string RedirectPage { get; set; }
    }
}
