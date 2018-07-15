using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KSCApp.Models
{
    public partial class Fixture
    {
        public Fixture()
        {
            Matches = new HashSet<Match>();
        }

        public int FixtureId { get; set; }
        public int LeagueId { get; set; }

        public int TeamAId { get; set; }
        [ForeignKey("TeamAId")]
        public virtual Team TeamA { get; set; }

        public int TeamBId { get; set; }
        [ForeignKey("TeamBId")]
        public virtual Team TeamB { get; set; }

        public DateTime PlayDate { get; set; }

        public League League { get; set; }


        public ICollection<Match> Matches { get; set; }
    }
}
