using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using Stock.Models;
using Stock.Services;

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
                var _result = await _AutrhenticationServiceProvider.Register(account.AccountName, account.AccountPassword,account.AccountWallet);
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
    }
}