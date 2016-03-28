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

    public class SoldShareViewModel : Share , IValidatableObject
    {
        public int NumberOfOwnedShares { get; set; }
        [Range(0,100,ErrorMessage = "The corrent value must be bewteen 0 and 100")]
        public int NumberOfSoldShares { get; set; }

        public SoldShareViewModel()
        {

        }

        public SoldShareViewModel(Share share,int numberOfOwnedShares)
        {
            this.CompanyCode = share.CompanyCode;
            this.CompanyName = share.CompanyName;
            this.PublicationDate = share.PublicationDate;
            this.UnitNumber = share.UnitNumber;
            this.UnitPrice = share.UnitPrice;
            this.NumberOfOwnedShares = numberOfOwnedShares;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (NumberOfOwnedShares < NumberOfSoldShares)
            {
                yield return new ValidationResult("You cannot sell more shares than you have", new[] { "NumberOfSoldShares" });
            }

            if (NumberOfSoldShares < 0)
            {
                yield return new ValidationResult("You cannot sell negative number of shares", new[] { "NumberOfSoldShares" });
            }
        }
    }
}