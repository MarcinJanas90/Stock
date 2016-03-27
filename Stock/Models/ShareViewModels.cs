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
        public int OwnedShareId { get; set; }
        public string CompanyCode { get; set; }
        public int NumberOfOwnedShares { get; set; }

        public OwnedShareViewModel()
        {

        }

        public OwnedShareViewModel(Share share)
        {
        }
    }

    public class BoughtShareViewModel :  Share
    {
        public int SharesBoughtAmount { get; set; }

        public BoughtShareViewModel()
        {
        }

        public BoughtShareViewModel(Share share)
        {
            ShareId = share.ShareId;
            CompanyName = share.CompanyName;
            CompanyCode = share.CompanyCode;
            PublicationDate = share.PublicationDate;
            UnitNumber = share.UnitNumber;
            UnitPrice = share.UnitPrice;
            SharesBoughtAmount = 0;
        }
    }
}