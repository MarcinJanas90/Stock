﻿using System;
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

        public async Task<HttpStatusCode> EditAccountName(string currentAccountName, string newAccountName, string accountPassword)
        {
            Account _account = await _applicationDbContext.Accounts.FirstOrDefaultAsync(x => x.AccountName == currentAccountName && x.AccountPassword == accountPassword);

            if (_account == null)
            {
                return HttpStatusCode.NotFound;
            }

            if(await _applicationDbContext.Accounts.FirstOrDefaultAsync(x=>x.AccountName==newAccountName)!= null)
            {
                return HttpStatusCode.BadRequest;
            }

            _account.AccountName = newAccountName;
            FormsAuthentication.SignOut();
            FormsAuthentication.SetAuthCookie(_account.AccountName, false);
            await _applicationDbContext.SaveChangesAsync();

            return HttpStatusCode.OK;        
        }

        public async Task<HttpStatusCode> EditAccountPassword(string accountName, string currentAccountPassword, string newAccountPassword, string confirmNewAccountPassword)
        {
            Account _account = await _applicationDbContext.Accounts.FirstOrDefaultAsync(x => x.AccountName == accountName && x.AccountPassword == currentAccountPassword);

            if (_account == null)
            {
                return HttpStatusCode.NotFound;
            }

            if (newAccountPassword != confirmNewAccountPassword)
            {
                return HttpStatusCode.BadRequest;
            }

            _account.AccountPassword = newAccountPassword;
            await _applicationDbContext.SaveChangesAsync();

            return HttpStatusCode.OK;
        }

        public async Task<HttpStatusCode> EditAccountWallet(string accountName, string accountPassword, double moneyToAdd, double moneyToSubtract)
        {
            Account _account = await _applicationDbContext.Accounts.FirstOrDefaultAsync(x => x.AccountName == accountName && x.AccountPassword == accountPassword);

            if (_account == null)
            {
                return HttpStatusCode.NotFound;
            }

            if ((moneyToAdd == 0 && moneyToSubtract == 0) || (moneyToAdd > 0 && moneyToSubtract > 0) || moneyToSubtract > _account.AccountWallet)
            {
                return HttpStatusCode.BadRequest;
            }

            if (moneyToAdd > 0)
            {
                _account.AccountWallet += moneyToAdd;
            }
            else
            {
                _account.AccountWallet -= moneyToSubtract;
            }

            await _applicationDbContext.SaveChangesAsync();

            return HttpStatusCode.OK;
        }

        public async Task<EditAccountWalletViewModel> GetCurrentWallet(string accountName)
        {
            Account _account = await _applicationDbContext.Accounts.FirstOrDefaultAsync(x => x.AccountName == accountName);

            if (_account == null)
            {
                return null;
            }

            EditAccountWalletViewModel _editAccountWalletViewModel = new EditAccountWalletViewModel();
            _editAccountWalletViewModel.CurrentWallet = _account.AccountWallet;

            return _editAccountWalletViewModel;
        }

        public async Task<HttpStatusCode> Login(string accountName, string accountPassword)
        {
            Account _account = await _applicationDbContext.Accounts.FirstOrDefaultAsync(x => x.AccountName == accountName && x.AccountPassword == accountPassword);

            if (_account == null)
            {
                return HttpStatusCode.NotFound;
            }

            if (_account.IsAuthenticated == true)
            {
                return HttpStatusCode.BadRequest;
            }

            FormsAuthentication.SetAuthCookie(accountName, false);

            _account.IsAuthenticated = true;
            await _applicationDbContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }

        public async Task<HttpStatusCode> Logout(string accountName)
        {
            Account _account = await _applicationDbContext.Accounts.FirstOrDefaultAsync(x => x.AccountName == accountName);

            if (_account == null)
            {
                return HttpStatusCode.NotFound;
            }

            FormsAuthentication.SignOut();
            _account.IsAuthenticated = false;
            await _applicationDbContext.SaveChangesAsync();

            return HttpStatusCode.OK;
        }

        public async Task<HttpStatusCode> Register(string accountName, string accountPassword,double accountWallet)
        {
            Account _account = await _applicationDbContext.Accounts.FirstOrDefaultAsync(x => x.AccountName == accountName);

            if (_account != null)
            {
                return HttpStatusCode.BadRequest;
            }

            _account = new Account(accountName,accountPassword);
            _account.AccountWallet = accountWallet;
            FormsAuthentication.SetAuthCookie(accountName, false);
            _account.IsAuthenticated = true;
            _applicationDbContext.Accounts.Add(_account);
            await _applicationDbContext.SaveChangesAsync();

            return HttpStatusCode.OK;
        }
    }
}