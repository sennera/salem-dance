using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalemDance.Models
{
    public class Review
    {
        public int ReviewID { get; set; }
        [StringLength(50)]
        [Display(Name = "Your Last Name")]
        [Column("LastName")]
        public string ReviewerLastName { get; set; }
        [StringLength(50), Required]
        [Display(Name = "Your First Name")]
        [Column("FirstName")]
        public string ReviewerFirstName { get; set; }
        [Range(1,5), Required]
        public int Stars { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        public string Comment { get; set; }
        public string Title { get; set; }

        [DataType(DataType.DateTime)]
        private DateTime _date = DateTime.Now;
        public DateTime Timestamp
        {
            get { return _date; }
            set { _date = value; }
        }
        public int? VenueID { get; set; }
        public int? EventID { get; set; }

        public virtual Venue Venue { get; set; }
        public virtual Event Event { get; set; }

    }
}