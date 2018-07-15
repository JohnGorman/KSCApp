using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KSCApp.Models
{
    public partial class RankHistory
    {
        public int RankHistoryId { get; set; }
        public int PlayerId { get; set; }
        public int OldRank { get; set; }
        public int NewRank { get; set; }
        public DateTime? DateChanged { get; set; }

        public Player Player { get; set; }
    }
}
