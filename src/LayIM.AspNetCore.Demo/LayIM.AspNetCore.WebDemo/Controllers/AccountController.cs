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
        /// <summary>
        /// 模拟登录
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public IActionResult Index(string uid)
        {
            if (string.IsNullOrEmpty(uid))
            {
                return Ok(new { msg = "请输入参数:uid" });
            }
            HttpContext.Session.SetString("layim_uid", uid);
            return Redirect("/");
        }
    }
}