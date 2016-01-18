using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalemDance.Models
{
    
    public class Event
    {
        public int EventID { get; set; }
        [Display(Name = "Venue")]
        public int VenueID { get; set; }
        [StringLength(100, MinimumLength = 3), Required]
        [Display(Name = "Event Name")]
        public string EventName { get; set; }
        // Should be a string as follows: 
        //M=Monday, T=Tuesday, W=Wednesday, R=Thursday, F=Friday, S=Saturday, N=Sunday
        //public DaysOfWeek? Days { get; set; }
        [Display(Name = "Start Time")]
        public string StartTime { get; set; }
        [Display(Name = "End Time")]
        public string EndTime { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public int? DanceStyleID { get; set; }

        [Display(Name = "Music Genre")]
        public string MusicGenre { get; set; }
        [Display(Name = "Entry Cost")]
        [DisplayFormat(NullDisplayText = "FREE")]
        [DataType(DataType.Currency)]
        [Column(TypeName="money")]
        public decimal? EntryCost { get; set; }
        [Display(Name = "Food Served")]
        public bool FoodServed { get; set; }
        [Display(Name = "Age Restriction")]
        public bool AgeRestriction { get; set; }
        [Range(0, 110)]
        [Display(Name = "Minimum Age")]
        [DisplayFormat(NullDisplayText = "None")]
        public int? MinAge { get; set; }
        [Display(Name = "Has Lessons")]
        public bool HasLessons { get; set; }


        public virtual Venue Venue { get; set; }
        public virtual DanceStyle DanceStyle { get; set; }
        public virtual ICollection<DayOpen> DaysOpen { get; set; }
    }
}