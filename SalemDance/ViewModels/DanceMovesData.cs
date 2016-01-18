using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SalemDance.Models;

namespace SalemDance.ViewModels
{
    public class DanceMovesData
    {
        public IEnumerable<DanceStyle> DanceStyles { get; set; }
        public IEnumerable<DanceMove> DanceMoves { get; set; }
    }
}