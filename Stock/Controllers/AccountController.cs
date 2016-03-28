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
        private IAuthenticationServiceProvider _AutrhenticationServiceProvider;

        public AccountController(IAuthenticationServiceProvider authenticationServiceProvider)
        {
            _AutrhenticationServiceProvider = authenticationServiceProvider;
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
                var _result = await _AutrhenticationServiceProvider.Login(account.AccountName, account.AccountPassword);
                var _status = _result.StatusCode;
                switch (_status)
                {
                    case 200:
                        return RedirectToAction("Index", "Stock");
                    default:
                        return _result;
                }
            }
            else
            {
                return View();
            }

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
                var _result = await _AutrhenticationServiceProvider.Register(account.AccountName, account.AccountPassword, account.AccountWallet);
                var _status = _result.StatusCode;

                switch (_status)
                {
                    case 200:
                        return RedirectToAction("Index", "Stock");
                    default:
                        return _result;
                }
            }
            else
            {
                return View();
            }
        }

        // GET: Account/Logout
        public async Task<ActionResult> Logout()
        {
            var _result = await _AutrhenticationServiceProvider.Logout(User.Identity.Name);
            var _status = _result.StatusCode;

            switch (_status)
            {
                case 200:
                    return RedirectToAction("Index", "Home");
                default:
                    return _result;
            }
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
                var Status = await _AutrhenticationServiceProvider.EditAccountName(User.Identity.Name, editAccountViewModel.NewAccountName, editAccountViewModel.AccountPassword);

                if (!Status.Equals(HttpStatusCode.OK))
                {
                    return new HttpStatusCodeResult(Status);
                }

                return RedirectToAction("Index", "Home");
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
                var Status = await _AutrhenticationServiceProvider.EditAccountPassword(User.Identity.Name, editAccountPasswordViewModel.CurrenrPasword, editAccountPasswordViewModel.NewPassowrd, editAccountPasswordViewModel.ConfirmNewPassword);

                if (!Status.Equals(HttpStatusCode.OK))
                {
                    return new HttpStatusCodeResult(Status);
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
            EditAccountWalletViewModel _editAccountWalletViewModel = new EditAccountWalletViewModel();
            _editAccountWalletViewModel.CurrentWallet = await _AutrhenticationServiceProvider.GetCurrentWallet(User.Identity.Name);
            return View(_editAccountWalletViewModel);

        }

        // POST: Account/EditAccountWallet/editAccountWalletViewModel
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> EditAccountWallet(EditAccountWalletViewModel _editAccountWalletViewModel)
        {
            if (ModelState.IsValid)
            {
                var Status = await _AutrhenticationServiceProvider.EditAccountWallet(User.Identity.Name, _editAccountWalletViewModel.AccountPassword, _editAccountWalletViewModel.WalletToAdd, _editAccountWalletViewModel.WalletToSubtract);

                if (!Status.Equals(HttpStatusCode.OK))
                {
                    return new HttpStatusCodeResult(Status);
                }

                return RedirectToAction("Index", "Stock");
            }

            return View();
        }

    }
}