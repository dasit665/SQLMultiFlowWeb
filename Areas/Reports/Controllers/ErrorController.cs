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
    public class ErrorController : Controller
    {
        public SQLMultyFlowWebContext DB { get; set; }

        public ErrorController(SQLMultyFlowWebContext DB)
        {
            this.DB = DB;
        }


        public IActionResult Index()
        {
            List<ErrorModel> model = (from errors in DB.TbErrors
                                      join versions in DB.TbScriptVersion on errors.ScriptVersionId equals versions.Id
                                      join scripts in DB.TbScriptsNames on versions.ScriptId equals scripts.Id
                                      join servers in DB.TbServerList on errors.ServerDbid equals servers.Id

                                      select new ErrorModel
                                      {
                                          Id = errors.Id,
                                          DateAndTime = errors.DateAndTime,
                                          ErrorCode = errors.ErrorCode,
                                          ErrorMessage = errors.ErrorMessage,
                                          ScriptName = scripts.ScriptName,
                                          ScriptVersion = versions.Virsion,
                                          ServerDb = $"{servers.ServerDomainName} {servers.DataBaseName}"
                                      }).ToList();

            return View(model);
        }
    }
}
