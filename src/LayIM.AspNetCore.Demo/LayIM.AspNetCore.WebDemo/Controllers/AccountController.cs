using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace LayIM.AspNetCore.WebDemo.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            HttpContext.Session.SetString("layim_uid", "123456");
            return Redirect("/");
        }
    }
}