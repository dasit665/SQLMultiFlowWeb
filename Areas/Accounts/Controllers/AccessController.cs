using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SQLMultiFlowWeb.Areas.Accounts.ViewsModels;

namespace SQLMultiFlowWeb.Areas.Accounts.Controllers
{
    [Area("Accounts")]
    public class AccessController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return await Task.Run(() =>
            {
                return View(new VMLogin());
            });
        }

        [HttpPost]
        public IActionResult Login(VMLogin loginUser)
        {
            if (ModelState.IsValid == true)
            {
                return Content($"{loginUser.Login} {loginUser.Password}");
            }
            else
            {
                return View(loginUser);
            }
        }
    }
}