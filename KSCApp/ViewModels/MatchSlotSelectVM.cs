using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KSCApp.ViewModels
{
    public class MatchSlotSelectVM
    {
        public int SelectedMatchId { get; set; }

        public string LeagueName { get; set; }

        public string MatchDetails { get; set; }

        public int? SelectedMatchSlotId { get; set; }
    }
}
