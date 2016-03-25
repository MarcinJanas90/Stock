using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stock.Models
{
    public class Stock
    {
        ICollection<Share> StockShares { get; set; }

        public Stock()
        {
            StockShares = new List<Share>();
        }
    }
}