using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stock.Models
{
    public class Stock
    {
        public ICollection<AvalaibleShareViewModel> StockShares { get; set; }

        public Stock()
        {
            StockShares = new List<AvalaibleShareViewModel>();
        }
    }
}