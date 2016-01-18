using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalemDance.ViewModels
{
    public class DaysWithEventData
    {
        public int DayID { get; set; }
        public DayOfWeek DayOpen { get; set; }
        public bool Assigned { get; set; }
    }
}