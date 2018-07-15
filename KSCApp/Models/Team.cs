using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KSCApp.Models
{
    public partial class Team
    {
        public Team()
        {
            //FixturesTeam = new HashSet<Fixture>();
            //FixturesTeamB = new HashSet<Fixture>();
            TeamPlayers = new HashSet<TeamPlayer>();
        }

        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public int TeamNo { get; set; }
        public byte[] TeamLogo { get; set; }
        public int LeagueId { get; set; }

        public League League { get; set; }
        //public ICollection<Fixture> FixturesTeam { get; set; }
        //public ICollection<Fixture> FixturesTeamB { get; set; }
        public ICollection<TeamPlayer> TeamPlayers { get; set; }
    }
}
