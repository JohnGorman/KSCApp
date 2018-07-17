using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KSCApp.Models
{
    public partial class LevelTime
    {
        public int LevelTimeId { get; set; }
        public int Level { get; set; }
        [Display(Name = "Start Time")]
        public TimeSpan StartTime { get; set; }
    }
}
