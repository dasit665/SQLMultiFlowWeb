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
    public class InfoController : Controller
    {
        public SQLMultyFlowWebContext DB { get; set; }

        public InfoController(SQLMultyFlowWebContext DB)
        {
            this.DB = DB;
        }


        public IActionResult Index()
        {
            List<InfoModel> model = DB.TbInfo.Select(s => new InfoModel { Id = s.Id, DataTime = s.DataTime, MessageInfo = s.MessageInfo, Script = s.Script, ServerDb = s.ServerDb }).ToList();


            return View(model);
        }
    }
}