using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Stock.Models
{
    [Table("Account")]
    public class Account
    {
        [Key]
        public int AccountID { get; set; }
        [Required(ErrorMessage = "This field cannot be empty")]
        [Column("AccountName")]
        [StringLength(20, MinimumLength = 2,ErrorMessage = "Entered name has wrong size, it should be between 2 and 20 characters")]
        [Display(Name = "Account Name")]
        public string AccountName { get; set; }
        [Required(ErrorMessage = "This field cannot be empty")]
        [Column("AccountPassword")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Entered password has wrong size, it should be between 2 and 20 characters")]
        [DataType(DataType.Password)]
        [Display(Name = "Account Password")]
        public string AccountPassword { get; set; }
        public bool IsAuthenticated { get; set; }
        public virtual ICollection<Connection> AccountConnections { get; set; }
        [Required]
        [Column("Wallet")]
        [DataType(DataType.Currency)]
        [Display(Name = "Wallet")]
        public double AccountWallet { get; set; }

        public virtual ICollection<OwnedShare> AccountOwnedShares { get; set; }

        public Account()
        {
            AccountOwnedShares = new List<OwnedShare>();
            AccountConnections = new List<Connection>();
        }
        
        public Account(string accountName,string accountPassword)
        {
            AccountOwnedShares = new List<OwnedShare>();
            AccountConnections = new List<Connection>();
            AccountName = accountName;
            AccountPassword = accountPassword;
        }
    }
}