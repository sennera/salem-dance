
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SalemDance.Models;

namespace SalemDance.ViewModels
{
    public class EventIndexData
    {

        public IEnumerable<Event> Events { get; set; }
        public IEnumerable<DanceStyle> DanceStyles { get; set; }
        public IEnumerable<Venue> Venues { get; set; }

       // public SelectList Actions { get; set; }
    }
}