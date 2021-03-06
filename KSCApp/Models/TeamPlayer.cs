﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KSCApp.Models
{
    public partial class TeamPlayer
    {
        public int TeamPlayerId { get; set; }

        public int TeamId { get; set; }
        public Team Team { get; set; }

        public int PlayerId { get; set; }
        public Player Player { get; set; }

        public int Level { get; set; }

        [Display(Name ="Matches Played")]
        public int? MatchesPlayed { get; set; }

        [Display(Name = "Matches Won")]
        public int? MatchesWon { get; set; }

        [Display(Name = "Games Won")]
        public int? GamesWon { get; set; }

        [Display(Name = "Games Lost")]
        public int? GamesLost { get; set; }

    }
}
