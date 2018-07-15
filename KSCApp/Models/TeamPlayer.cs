using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KSCApp.Models
{
    public partial class TeamPlayer
    {
        public int TeamPlayerId { get; set; }
        public int Level { get; set; }
        public int? MatchesPlayed { get; set; }
        public int? MatchesWon { get; set; }
        public int? GamesWon { get; set; }
        public int? GamesLost { get; set; }
        public int TeamId { get; set; }
        public int PlayerId { get; set; }

        public Player Player { get; set; }
        public Team Team { get; set; }
    }
}
