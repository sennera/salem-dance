using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalemDance.Models
{
    public class DayOpen
    {
        public int ID { get; set; }
        public DayOfWeek DayOfWeek { get; set; }

        public virtual ICollection<Event> Events { get; set; }

    }
}