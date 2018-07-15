using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KSCApp.Models
{
    public partial class GameResult
    {
        public int GameResultId { get; set; }
        public int MatchId { get; set; }
        public int GameNo { get; set; }
        public int PlayerApoints { get; set; }
        public int PlayerBpoints { get; set; }

        public Match Match { get; set; }
    }
}
