using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Stock.Models;

namespace Stock.Services
{
    public interface IAuthenticationService
    {
        Task<bool> Login(string accountName, string accountPassword);
        Task<bool> Register(string accountName, string accountPassword, double accountWallet);
        Task<bool> Logout(string accountName);
        Task<bool> EditAccountName(string currentAccountName, string newAccountName, string accountPassword);
        Task<bool> EditAccountPassword(string accountName, string currentAccountPassword, string newAccountPassword,string confirmNewAccountPassword);
        Task<bool> EditAccountWallet(string accountName, string accountPassword, double moneyToAdd, double moneyToSubtract);
        Task<EditAccountWalletViewModel> GetCurrentWallet(string accountName);
    }
}
