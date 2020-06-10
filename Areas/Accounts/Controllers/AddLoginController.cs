using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SQLMultiFlowWeb.Areas.Accounts.ViewsModels;

using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace SQLMultiFlowWeb.Areas.Accounts.Controllers
{
    [Area("Accounts")]
    public class AddLoginController : Controller
    {
        public SQLMultyFlowWebContext DB { get; set; }

        public AddLoginController(SQLMultyFlowWebContext SQLContext)
        {
            this.DB = SQLContext;
        }

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
                TbLogins loginToAdd = new TbLogins();

                loginToAdd.UserName = createdLogin.Login;

                using (var hash = SHA256.Create())
                {
                    var hashBytes = hash.ComputeHash(Encoding.Unicode.GetBytes(createdLogin.Password));
                    loginToAdd.HashPasswors = Encoding.Unicode.GetString(hashBytes);
                }

                DB.TbLogins.Add(loginToAdd);
                DB.SaveChanges();

                return RedirectToRoute("login");

            }
            else
            {
                return View(createdLogin);
            }
        }
    }
}