using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KSCApp.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using KSCApp.ViewModels;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace KSCApp.ViewModels
{
    public class ScheduleVM
    {

        public int MatchSlotId { get; set; }

        [Display(Name ="Fixture Details")]
        public string FixtureDetails { get; set; }

        [Display(Name = "Match Details")]
        public string MatchDetails { get; set; }

        [DisplayFormat(DataFormatString = "{0:hh\\:mm tt}")]
        public DateTime MatchStart { get; set; }

        public int? LeagueId { get; set; }
    }

}
