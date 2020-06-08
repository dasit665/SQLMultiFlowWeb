using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SQLMultiFlowWeb.Areas.Accounts.ViewsModels;

using System.Security.Cryptography;
using System.Text;

namespace SQLMultiFlowWeb.Areas.Accounts.Controllers
{
    [Area("Accounts")]
    public class AddLoginController : Controller
    {
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(VMAddLogin createdLogin)
        {
            if (ModelState.IsValid == true)
            {
                using (SQLMultyFlowWebContext DB = new SQLMultyFlowWebContext())
                {
                    TbLogins loginToAdd = new TbLogins();

                    loginToAdd.UserName = createdLogin.Login;

                    using (var hash = SHA256.Create())
                    {
                        var hashBytes = hash.ComputeHash(Encoding.Default.GetBytes(createdLogin.Password));
                        loginToAdd.HashPasswors = Encoding.Default.GetString(hashBytes);
                    }

                    DB.TbLogins.Add(loginToAdd);
                    DB.SaveChanges();
                }

                return RedirectToRoute("login");

            }
            else
            {
                return View(createdLogin);
            }
        }
    }
}