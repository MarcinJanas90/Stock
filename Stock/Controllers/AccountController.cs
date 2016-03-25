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
                return await _AutrhenticationServiceProvider.Login(account.AccountName, account.AccountPassword);
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
                return await _AutrhenticationServiceProvider.Register(account.AccountName, account.AccountPassword);
            }
            else
            {
                return View();
            }
        }

        // GET: Action/Logout
        public async Task<ActionResult> Logout()
        {
            return await _AutrhenticationServiceProvider.Logout(User.Identity.Name);
        }
    }
}