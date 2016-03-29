using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Stock.Models
{
    public class BoughtShareViewModel : IValidatableObject
    {
        [Display(Name = "Company")]
        public virtual string CompanyName { get; set; } 
        [Display(Name = "Company Code")]
        public string CompanyCode { get; set; }
        [Display(Name = "Units number")]
        public int UnitNumber { get; set; }
        [Display(Name = "Value")]
        public double UnitPrice { get; set; }
        [DataType(DataType.DateTime)]
        [Display(Name = "Publication Date")]
        public DateTime PublicationDate { get; set; }
        public int SharesBoughtAmount { get; set; }

        public BoughtShareViewModel()
        {
        }

        public BoughtShareViewModel(Share share)
        {
            CompanyName = share.CompanyName;
            CompanyCode = share.CompanyCode;
            PublicationDate = share.PublicationDate;
            UnitNumber = share.UnitNumber;
            UnitPrice = share.UnitPrice;
            SharesBoughtAmount = 0;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (SharesBoughtAmount > UnitNumber)
            {
                yield return new ValidationResult("You cannot buy more than is avalaible", new[] { "SharesBoughtAmount" });
            }

            if (SharesBoughtAmount < 0)
            {
                yield return new ValidationResult("You cannot buy negative number of shares", new[] { "SharesBoughtAmount" });
            }
        }
    }

    public class SoldShareViewModel : IValidatableObject
    {
        [Display(Name = "Company")]
        public virtual string CompanyName { get; set; }
        [Display(Name = "Company Code")]
        public string CompanyCode { get; set; }
        [Display(Name = "Units number")]
        public int UnitNumber { get; set; }
        [Display(Name = "Value")]
        public double UnitPrice { get; set; }
        [DataType(DataType.DateTime)]
        [Display(Name = "Publication Date")]
        public DateTime PublicationDate { get; set; }
        public int NumberOfOwnedShares { get; set; }
        public int NumberOfSoldShares { get; set; }
        public double TotalValue { get; set; }

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
            this.TotalValue = UnitPrice * NumberOfOwnedShares;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (NumberOfSoldShares > NumberOfOwnedShares)
            {
                yield return new ValidationResult("You cannot sell more than you have", new[] { "NumberOfSoldShares" });
            }

            if (NumberOfSoldShares < 0)
            {
                yield return new ValidationResult("You cannot sell negative number of shares", new[] { "NumberOfSoldShares" });
            }
        }
    }

}