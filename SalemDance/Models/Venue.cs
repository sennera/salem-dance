using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalemDance.Models
{
    public class Venue
    {
        public int VenueID { get; set; }
        [StringLength(50), Required]
        [Display(Name = "Venue Name")]
        public string VenueName { get; set; }
        public string Address { get; set; }
        [DataType(DataType.Url)]
        public string Website { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }


        public virtual ICollection<DanceMove> DanceMoves { get; set; }
    }
}