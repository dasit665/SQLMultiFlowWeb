using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;

namespace SQLMultiFlowWeb.Areas.Reprocessing.Controllers
{
    [Area("Reprocessing")]
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
            var res = (from scriptFlowed in DB.TbFlowed
                       join scriptsVersion in DB.TbScriptVersion on scriptFlowed.ScriptVersionId equals scriptsVersion.Id
                       join scriptsNames in DB.TbScriptsNames on scriptsVersion.ScriptId equals scriptsNames.Id
                       join serversAndDB in DB.TbServerList on scriptFlowed.ServerDbid equals serversAndDB.Id
                       where scriptFlowed.VersionFlowed == false
                       select new Models.NonFlowedScriptsVersions()
                       {
                           ID = scriptFlowed.Id,
                           ScriptName = scriptsNames.ScriptName,
                           ScriptVersion = scriptsVersion.Virsion,
                           DateOfTry = scriptFlowed.DateOfTry,
                           ServerName = serversAndDB.ServerDomainName,
                           ServerDB = serversAndDB.DataBaseName
                       }).ToList();

            return View(res);
        }
    }
}
