using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stock.Models
{
    public class OwnedShareViewModel
    {
        public int OwnedSharesNumber { get; set; }
        public Share OwnedShare { get; set; }
    }
}