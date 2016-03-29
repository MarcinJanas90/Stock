using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Stock.Models
{
    [Table("Share")]
    public class Share
    {
        [Key]
        public int ShareId { get; set; }
        [Column("CompanyName")]
        [Display(Name = "Company")]
        public virtual string CompanyName { get; set; }
        [Column("CompanyCode")]
        [Display(Name = "Company Code")]
        public string CompanyCode { get; set; }
        [Column("UnitNumber")]
        [Display(Name = "Units number")]
        public int UnitNumber { get; set; }
        [Column("ShareValue")]
        [Display(Name =  "Value")]
        public double UnitPrice { get; set; }
        [DataType(DataType.DateTime)]
        [Column("PublicationDate")]
        [Display(Name = "Publication Date")]
        public DateTime PublicationDate { get; set; }
    }
}