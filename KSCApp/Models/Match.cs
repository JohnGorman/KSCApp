using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KSCApp.Models
{
    public partial class Match
    {
        public Match()
        {
            GameResults = new HashSet<GameResult>();
            MatchSlots = new HashSet<MatchSlot>();
        }

        public int MatchId { get; set; }
        public int? FixtureId { get; set; }
        //public DateTime? MatchDateTime { get; set; }

        public int PlayerAId { get; set; }
        [ForeignKey("PlayerAId")]
        public Player PlayerA { get; set; }

        public int PlayerBId { get; set; }
        [ForeignKey("PlayerBId")]
        public Player PlayerB { get; set; }


        public int? Level { get; set; }
        public bool? Played { get; set; }
        public DateTime? PlayedDate { get; set; }
        public int PlayerAgames { get; set; }
        public int PlayerBgames { get; set; }

        public Fixture Fixture { get; set; }


        public ICollection<GameResult> GameResults { get; set; }
        public ICollection<MatchSlot> MatchSlots { get; set; }
    }
}
