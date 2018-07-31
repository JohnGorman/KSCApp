using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KSCApp.ViewModels
{
    public class TeamVM
    {
        public int LeagueId { get; set; }

        public int TeamId { get; set; }

        [Display(Name = "Team Name")]
        public string TeamName { get; set; }

        [Display(Name = "Team No")]
        public int TeamNo { get; set; }

        public string Section { get; set; }

        [Display(Name = "Matches Played")]
        public int? MatchesPlayed { get; set; }

        [Display(Name = "Matches Won")]
        public int? MatchesWon { get; set; }

        [Display(Name = "Games Won")]
        public int? GamesWon { get; set; }

        [Display(Name = "Games Lost")]
        public int? GamesLost { get; set; }


    }
}
