using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KSCApp.ViewModels
{
    public class ScheduleSelectVM
    {
        public int? CurrentLeagueId { get; set; }

        [DataType(DataType.Date), Display(Name = "Select Date")]
        public DateTime SelectedDate { get; set; }
    }
}
