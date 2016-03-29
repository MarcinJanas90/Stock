using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Stock.Models
{
    [Table("OnwedShare")]
    public class OwnedShare
    {
        [Key]
        public int OwnedShareId { get; set; }
        [Required]
        [Column("OwnedShareCompanyCode")]
        [Display(Name = "Company code")]
        public string OwnedShareCompanyCode { get; set; }
        [Required]
        [Column("NumberOfOwnedShares")]
        [Display(Name = "Number of owned shares")]
        public int NumberOfOwnedShares { get; set; } 

    }
}