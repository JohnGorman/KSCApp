using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KSCApp.ViewModels
{
    public class MatchResultVM
    {
        public string FixtureDetails { get; set; }
        public string MatchDetails { get; set; }
        public string GameResults { get; set; }
        public int MatchId { get; set; }
        public string DatePlayed { get; set; }
    }
}
