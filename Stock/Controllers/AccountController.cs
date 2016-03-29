using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using Stock.Models;
using Stock.Services;
using System.Net;

namespace Stock.Controllers
{
    public class AccountController : Controller
    {
        private IAuthenticationService _AuthenticationServiceProvider;

        public AccountController(IAuthenticationService authenticationServiceProvider)
        {
            _AuthenticationServiceProvider = authenticationServiceProvider;
        }

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        // GET: Account/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        public async Task<ActionResult> Login(Account account)
        {
            if (ModelState.IsValid)
            {
                var Status = await _AuthenticationServiceProvider.Login(account.AccountName, account.AccountPassword);

                if (Status)
                {
                    return RedirectToAction("Index", "Stock");
                }

                return View();
            }

            return View();

        }

        // GET: Account/Register
        public ActionResult Register()
        {
            return View();
        }

        // POST: Account/Register
        [HttpPost]
        public async Task<ActionResult> Register(Account account)
        {
            if (ModelState.IsValid)
            {
                var Status = await _AuthenticationServiceProvider.Register(account.AccountName, account.AccountPassword, account.AccountWallet);

                if (Status)
                {
                    return RedirectToAction("Index", "Stock");
                }

                return View();
            }

            return View();
        }

        // GET: Account/Logout
        public async Task<ActionResult> Logout()
        {
            var Status = await _AuthenticationServiceProvider.Logout(User.Identity.Name);

            if (Status)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        // GET: Account/EditAccountName
        [Authorize]
        public ActionResult EditAccountName()
        {
            return View(new EditAccountNameViewModel());
        }

        // POST: Account/EditAccountName/editAccountViewModel
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> EditAccountName(EditAccountNameViewModel editAccountViewModel)
        {
            if (ModelState.IsValid)
            {
                var Status = await _AuthenticationServiceProvider.EditAccountName(User.Identity.Name, editAccountViewModel.NewAccountName, editAccountViewModel.AccountPassword);

                if (Status)
                {
                    return RedirectToAction("Index", "Stock");
                }

                return View();
            }
            else
            {
                return View();
            }
        }

        // GET: Account/EditAccountPassword
        [Authorize]
        public ActionResult EditAccountPassword()
        {
            return View(new EditAccountPasswordViewModel());
        }

        // GET: Account/EditAccountPassword
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> EditAccountPassword(EditAccountPasswordViewModel editAccountPasswordViewModel)
        {
            if (ModelState.IsValid)
            {
                var Status = await _AuthenticationServiceProvider.EditAccountPassword(User.Identity.Name, editAccountPasswordViewModel.CurrenrPasword, editAccountPasswordViewModel.NewPassowrd, editAccountPasswordViewModel.ConfirmNewPassword);

                if (Status.Equals(HttpStatusCode.OK))
                {
                    return RedirectToAction("Index", "Stock");
                }

                return RedirectToAction("Index", "Stock");
            }
            else
            {
                return View();
            }
        }

        // GET: Account/EditAccountWallet
        [Authorize]
        public async Task<ActionResult> EditAccountWallet()
        {
            return View(await _AuthenticationServiceProvider.GetCurrentWallet(User.Identity.Name));
        }

        // POST: Account/EditAccountWallet/editAccountWalletViewModel
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> EditAccountWallet(EditAccountWalletViewModel _editAccountWalletViewModel)
        {
            if (ModelState.IsValid)
            {
                var Status = await _AuthenticationServiceProvider.EditAccountWallet(User.Identity.Name, _editAccountWalletViewModel.AccountPassword, _editAccountWalletViewModel.WalletToAdd, _editAccountWalletViewModel.WalletToSubtract);

                if (Status)
                {
                    return RedirectToAction("Index", "Stock");
                }
                return View();
            }

            return View();
        }

    }
}