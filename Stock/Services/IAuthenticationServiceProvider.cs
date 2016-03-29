using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Stock.Services
{
    public interface IAuthenticationServiceProvider
    {
        Task<HttpStatusCode> Login(string accountName, string accountPassword);
        Task<HttpStatusCode> Register(string accountName, string accountPassword, double accountWallet);
        Task<HttpStatusCode> Logout(string accountName);
        Task<HttpStatusCode> EditAccountName(string currentAccountName, string newAccountName, string accountPassword);
        Task<HttpStatusCode> EditAccountPassword(string accountName, string currentAccountPassword, string newAccountPassword,string confirmNewAccountPassword);
        Task<HttpStatusCode> EditAccountWallet(string accountName, string accountPassword, double moneyToAdd, double moneyToSubtract);
        Task<double> GetCurrentWallet(string accountName);
    }
}
