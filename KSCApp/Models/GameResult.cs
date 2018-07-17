using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KSCApp.Models
{
    public partial class GameResult
    {
        public int GameResultId { get; set; }
        public int MatchId { get; set; }
        [Display(Name = "Game No")]
        public int GameNo { get; set; }
        [Display(Name = "Player A Points")]
        public int PlayerApoints { get; set; }
        [Display(Name = "Player B Points")]
        public int PlayerBpoints { get; set; }

        public Match Match { get; set; }
    }
}
