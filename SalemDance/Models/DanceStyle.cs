using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalemDance.Models
{
    public class DanceStyle
    {
        public int DanceStyleID { get; set; }
        [StringLength(50), Required]
        [Display(Name = "Style Name")]
        public string StyleName { get; set; }
        [StringLength(50), Required]
        [Display(Name = "Music Genre")]
        public String MusicGenre { get; set; }

        public virtual ICollection<DanceMove> DanceMoves { get; set; }
    }
}