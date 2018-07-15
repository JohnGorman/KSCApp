using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KSCApp.Models
{
    public partial class MatchSlot
    {
        public MatchSlot()
        {
            
        }

        public int MatchSlotId { get; set; }
        public int? MatchId { get; set; }
        public DateTime MyProperty { get; set; }
        public int? SlotMinutes { get; set; }

        public Match Match { get; set; }


        
    }
}
