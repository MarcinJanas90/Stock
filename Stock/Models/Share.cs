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
        [Required]
        [Column("CompanyName")]
        [Display(Name = "Company")]
        public string CompanyName { get; set; }
        [Required]
        [Column("CompanyCode")]
        [Display(Name = "Company Code")]
        public string CompanyCode { get; set; }
        [Required]
        [Column("UnitNumber")]
        [Display(Name = "Units number")]
        public int UnitNumber { get; set; }
        [Required]
        [Column("ShareValue")]
        [Display(Name =  "Value")]
        public double UnitPrice { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        [Column("PublicationDate")]
        [Display(Name = "Publication Date")]
        public DateTime PublicationDate { get; set; }
    }
}