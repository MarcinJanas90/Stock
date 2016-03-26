using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Stock.Models
{
    public class OwnedShareViewModel
    {
        [Key]
        public int OwnedSharesId { get; set; }
        public int OwnedSharesNumber { get; set; }
        public Share OwnedShare { get; set; }
    }
}