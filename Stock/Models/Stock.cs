using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stock.Models
{
    public class Stock
    {
        public ICollection<Share> CurrentShares { get; set; }

        public Stock()
        {
            CurrentShares = new List<Share>();
        }
    }
}