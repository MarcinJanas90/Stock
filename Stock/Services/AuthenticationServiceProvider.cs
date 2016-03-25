using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Stock.Models;
using System.Data.Entity;
using System.Net;
using System.Web.Security;

namespace Stock.Services
{
    public class AuthenticationServiceProvider : IAuthenticationServiceProvider
    {
        private ApplicationDbContext _applicationDbContext;

        public AuthenticationServiceProvider()
        {
            _applicationDbContext = new ApplicationDbContext();
        }

        public async Task<HttpStatusCodeResult> Login(string accountName, string accountPassword)
        {
            Account _account = await _applicationDbContext.Accounts.FirstOrDefaultAsync(x => x.AccountName == accountName && x.AccountPassword == accountPassword);

            if (_account == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            if (_account.IsAuthenticated == true)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            FormsAuthentication.SetAuthCookie(accountName, false);

            _account.IsAuthenticated = true;
            await _applicationDbContext.SaveChangesAsync();

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        public async Task<HttpStatusCodeResult> Logout(string accountName)
        {
            Account _account = await _applicationDbContext.Accounts.FirstOrDefaultAsync(x => x.AccountName == accountName);

            if (_account == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            FormsAuthentication.SignOut();
            _account.IsAuthenticated = false;
            await _applicationDbContext.SaveChangesAsync();

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        public async Task<HttpStatusCodeResult> Register(string accountName, string accountPassword)
        {
            Account _account = await _applicationDbContext.Accounts.FirstOrDefaultAsync(x => x.AccountName == accountName);

            if (_account != null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            _account = new Account(accountName,accountPassword);
            FormsAuthentication.SetAuthCookie(accountName, false);
            _account.IsAuthenticated = true;
            _applicationDbContext.Accounts.Add(_account);
            await _applicationDbContext.SaveChangesAsync();

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}