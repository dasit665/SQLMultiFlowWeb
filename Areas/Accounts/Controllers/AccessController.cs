using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using SQLMultiFlowWeb.Areas.Accounts.ViewsModels;

namespace SQLMultiFlowWeb.Areas.Accounts.Controllers
{
    [Area("Accounts")]
    public class AccessController : Controller
    {
        public SQLMultyFlowWebContext DB { get; set; }

        public AccessController(SQLMultyFlowWebContext SQLContext)
        {
            this.DB = SQLContext;
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return await Task.Run(() =>
            {
                return View(new VMLogin());
            });
        }

        [HttpPost]
        public async Task<IActionResult> Login(VMLogin loginUser)
        {
            if (ModelState.IsValid == true)
            {
                var hashPassword = "";

                using (var hash = SHA256.Create())
                {
                    hashPassword = Encoding.Unicode.GetString(hash.ComputeHash(Encoding.Unicode.GetBytes(loginUser.Password)));
                }

                var loginInDB = DB.TbLogins.Where(w => w.UserName == loginUser.Login && w.HashPasswors == hashPassword).FirstOrDefault();

                if (loginInDB is null == false)
                {
                    await Identification(loginInDB);

                    return RedirectToRoute("default1");
                }
                else
                {
                    Response.Headers.Add("REFRESH", $"2.5;Login");
                    return View("LoginIncorrect");
                }
            }
            else
            {
                return View(loginUser);
            }
        }

        [NonAction]
        public async Task Identification(TbLogins login)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, login.UserName),
                new Claim(ClaimTypes.Role, "User"),
                new Claim("hashPassword", login.HashPasswors)
            };

            ClaimsIdentity ID = new ClaimsIdentity(claims, "ApplicationCookie", ClaimTypes.Name, ClaimTypes.Role);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(ID));
        }

        [Route("/LogOut")]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToRoute("login");
        }
    }
}