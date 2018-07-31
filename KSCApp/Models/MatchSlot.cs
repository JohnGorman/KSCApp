using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KSCApp.Models
{
    public partial class MatchSlot
    {
        //public MatchSlot()
        //{
        //    Matches = new HashSet<Match>();
        //}

        public int MatchSlotId { get; set; }

        [Display(Name ="Booking Slot")]
        public DateTime BookingSlot { get; set; }

        [Display(Name = "Slot Minutes")]
        public int? SlotMinutes { get; set; }

        public int? MatchId { get; set; }
        public Match Match { get; set; }


        //public ICollection<Match> Matches { get; set; }
    }
}
