using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SQLMultiFlowWeb.Areas.Reports.Models;

namespace SQLMultiFlowWeb.Areas.Reports.Controllers
{
    [Area("Reports")]
    [Authorize]
    public class FlowedController : Controller
    {
        public SQLMultyFlowWebContext DB { get; set; }

        public FlowedController(SQLMultyFlowWebContext DB)
        {
            this.DB = DB;
        }


        public IActionResult Index()
        {
            List<FlowedModel> model = (from flowed in DB.TbFlowed
                                       join scriptVersion in DB.TbScriptVersion on flowed.ScriptVersionId equals scriptVersion.Id
                                       join scriptName in DB.TbScriptsNames on scriptVersion.ScriptId equals scriptName.Id
                                       join server in DB.TbServerList on flowed.ServerDbid equals server.Id

                                       select new FlowedModel
                                       {
                                           Id = flowed.Id,
                                           DateOfTry = flowed.DateOfTry,
                                           ScriptName = scriptName.ScriptName,
                                           ScriptVersion = scriptVersion.Virsion,
                                           ServerNameDB = $"{server.ServerDomainName} {server.DataBaseName}",
                                           VersionFlowed = flowed.VersionFlowed
                                       }).ToList();


            return View(model);
        }
    }
}
