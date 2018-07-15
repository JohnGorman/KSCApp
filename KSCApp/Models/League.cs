using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KSCApp.Models
{
    public enum LeagueType { Short, Long}

    public partial class League
    {
        public League()
        {
            Fixtures = new HashSet<Fixture>();
            Teams = new HashSet<Team>();
            this.FixturesMade = false;
        }

        public int LeagueId { get; set; }

        [Display(Name ="League Name")]
        public string LeagueName { get; set; }

        [DataType(DataType.Date), Display(Name ="Start Date")]
        public DateTime StartDate { get; set; }

        [Required, Display(Name = "Active")]
        public bool Active { get; set; }

        [Display(Name = "No of Levels")]
        public int NoOfLevels { get; set; }

        [Required, Display(Name = "Fixtures Made")]
        public bool FixturesMade { get; set; }

        [EnumDataType(typeof(LeagueType)), Display(Name = "League Type")]
        public LeagueType LeagueType { get; set; }

        public ICollection<Fixture> Fixtures { get; set; }
        public ICollection<Team> Teams { get; set; }
    }
}
