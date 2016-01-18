using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalemDance.Models
{
    public class DanceMove
    {
        public int DanceMoveID { get; set; }
        public int DanceStyleID { get; set; }
        [StringLength(50), Required]
        [Display(Name = "Dance Move Name")]
        public string DanceMoveName { get; set; }
        [DataType(DataType.Url)]
        public string Link { get; set; }

        public virtual DanceStyle DanceStyle { get; set; }
    }
}