using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KSCApp.Models;

namespace KSCApp.ViewModels
{
    public class ResultsVM
    {
        public int MatchId { get; set; }

        public int Game1PlayerAScore { get; set; }

        public int Game1PlayerBScore { get; set; }

        public int Game2PlayerAScore { get; set; }

        public int Game2PlayerBScore { get; set; }

        public int Game3PlayerAScore { get; set; }

        public int Game3PlayerBScore { get; set; }

        public int Game4PlayerAScore { get; set; }

        public int Game4PlayerBScore { get; set; }

        public int Game5PlayerAScore { get; set; }

        public int Game5PlayerBScore { get; set; }
    }
}
