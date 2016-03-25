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
        public string ShareId { get; set; }
        [Required]
        [Column("CompanyName")]
        [Display(Name = "Company")]
        public string CompanyName { get; set; }
        [Required]
        [Column("ShareValue")]
        [Display(Name =  "Value")]
        public string ShareValue { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        [Column("UpdateDate")]
        [Display(Name = "UpdateTime")]
        public DateTime ShareLastUpdateDateTime { get; set; }
    }
}