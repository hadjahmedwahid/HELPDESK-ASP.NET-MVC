using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelpDesk.Models
{
    public class Statistic : HelpDeskDB
    {
        public int IdStat { get; set; }
        public DateTime DateStat { get; set; }
        
    }
}