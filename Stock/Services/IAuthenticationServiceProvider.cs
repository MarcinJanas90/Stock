﻿using System;
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
        Task<HttpStatusCodeResult> Login(string accountName, string accountPassword);
        Task<HttpStatusCodeResult> Register(string accountName, string accountPassword);
        Task<HttpStatusCodeResult> Logout(string accountName);
    }
}