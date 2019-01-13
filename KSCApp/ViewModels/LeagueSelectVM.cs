using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KSCApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KSCApp.ViewModels
{
    public class LeagueSelectVM
    {
        public League SelectedLeague { get; set; }
        public string SelectedLeagueId { get; set; }
        public IEnumerable<SelectListItem> LeagueSelectList { get; set; }

        [DataType(DataType.Date), Display(Name = "Select Date"), Required]
        public DateTime SelectedDate { get; set; }

    }
}
