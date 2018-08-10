using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KSCApp.Models
{
    public class FixtureDate
    {
        public FixtureDate()
        {

        }

        public int FixtureDateId { get; set; }

        [Required, Display(Name ="League Type")]
        public LeagueType LeagueType { get; set; }

        [Required, Display(Name = "Team A No")]
        public int TeamANo { get; set; }

        [Required, Display(Name = "Team B No")]
        public int TeamBNo { get; set; }

        [Required, Display(Name = "Start Plus(Days)")]
        public int StartDaysPlus { get; set; }

    }
}
