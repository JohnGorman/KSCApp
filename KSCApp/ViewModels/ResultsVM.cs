using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KSCApp.Models;

namespace KSCApp.ViewModels
{
    public class Game
    {
        public int GameNo { get; set; }
        public int PlayerAScore { get; set; }
        public int PlayerBScore { get; set; }

    }

    public class ResultsVM
    {
        public int MatchId { get; set; }
        public int TeamPlayerAId { get; set; }
        public int TeamPlayerBId { get; set; }

    }
}
