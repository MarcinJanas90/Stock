using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Stock.Models
{
    public class EditAccountNameViewModel
    { 
        [Required]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Entered name has wrong size, it should be between 2 and 20 characters")]
        [Display(Name = "New Account Name")]
        public string NewAccountName { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Entered password has wrong size, it should be between 2 and 20 characters")]
        [DataType(DataType.Password)]
        [Display(Name = "Account Password")]
        public string AccountPassword { get; set; }
    }

    public class EditAccountPasswordViewModel
    {
        [Required]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Entered password has wrong size, it should be between 2 and 20 characters")]
        [DataType(DataType.Password)]
        [Display(Name = "Current Password")]
        public string CurrenrPasword { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Entered password has wrong size, it should be between 2 and 20 characters")]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string NewPassowrd { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Entered password has wrong size, it should be between 2 and 20 characters")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm New Password")]
        public string ConfirmNewPassword { get; set; }
    }

    public class EditAccountWalletViewModel
    {
        [DataType(DataType.Currency)]
        [Display(Name = "Current money")]
        public double CurrentWallet { get; set; }
        [DataType(DataType.Currency)]
        [Display(Name = "Money to add")]
        public double WalletToAdd { get; set; }
        [DataType(DataType.Currency)]
        [Display(Name = "Money to subtract")]
        public double WalletToSubtract { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Entered password has wrong size, it should be between 2 and 20 characters")]
        [DataType(DataType.Password)]
        [Display(Name = "Account Password")]
        public string AccountPassword { get; set; }
    }
}