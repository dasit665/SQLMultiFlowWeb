using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SQLMultiFlowWeb.Areas.Processing.Models;

namespace SQLMultiFlowWeb.Areas.Processing.Controllers
{
    [Area("Processing")]
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
            var scriptsAndServices = new ScriptsServers();

            scriptsAndServices.ScriptsNames = DB.TbScriptsNames.Select(s => s.ScriptName).ToList();
            scriptsAndServices.Markers = DB.TbMarkers.Select(s => s.MarkerName).ToList();
            scriptsAndServices.Servers = (from server in DB.TbServerList
                                          select new Server()
                                          {
                                              ServerDomainName = server.ServerDomainName,
                                              DataBaseName = server.DataBaseName,
                                              ServerLogin = server.ServerLogin,
                                              ServerPassword = server.ServerPassword
                                          }).ToList();

            return View(scriptsAndServices);
        }
    }
}
