using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Stock.Models
{
    public class OwnedShareViewModel : Share
    {
        public int NumberOfOwnedShares { get; set; }
        public double TotalValue { get; set; }
    }
}