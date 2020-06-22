using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;

namespace SQLMultiFlowWeb.Areas.Scripts.Controllers
{
    [Area("Scripts")]
    [Authorize]
    public class HomeController : Controller
    {
        public SQLMultyFlowWebContext DB { get; set; }

        public HomeController(SQLMultyFlowWebContext DB)
        {
            this.DB = DB;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> GetScriptsNames()
        {
            return await Task.Run(() =>
            {
                var model = (from versions in DB.TbScriptVersion
                             join scripts in DB.TbScriptsNames on versions.ScriptId equals scripts.Id

                             select new
                             {
                                 ScriptName = scripts.ScriptName
                             }).Distinct().ToList();

                Response.ContentType = "application/json";
                return Content(JsonConvert.SerializeObject(model));
            });
        }

        [HttpPost]
        public async Task<IActionResult> GetScriptsVersions()
        {
            return await Task.Run(() =>
            {
                var model = (from versions in DB.TbScriptVersion
                             join scripts in DB.TbScriptsNames on versions.ScriptId equals scripts.Id

                             where scripts.ScriptName == Request.Form["ScriptName"].ToString()

                             select versions.Virsion).ToList();

                Response.ContentType = "application/json";

                return Content(JsonConvert.SerializeObject(model));
            });
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> GetScriptsContent()
        {
            return await Task.Run(() =>
            {
                var ScriptName = Request.Form["ScriptName"];
                var ScriptVersion = Convert.ToDecimal(Request.Form["ScriptVersion"]);

                var model = (from version in DB.TbScriptVersion
                             join script in DB.TbScriptsNames on version.ScriptId equals script.Id
                             select new
                             {
                                 ScriptName = script.ScriptName,
                                 ScriptVersion = version.Virsion,
                                 Content = version.ScriptContent
                             })
                             .ToList()
                             .Where(w => w.ScriptVersion == ScriptVersion)
                             .Where(w => w.ScriptName == ScriptName)
                             .Select(s => s.Content)
                             .FirstOrDefault();


                return Content(model);

            });
        }
    }
}