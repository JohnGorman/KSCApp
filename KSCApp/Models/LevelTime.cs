using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KSCApp.Models
{
    public partial class LevelTime
    {
        public int LevelTimeId { get; set; }
        public int Level { get; set; }
        public TimeSpan StartTime { get; set; }
    }
}
