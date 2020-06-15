using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipelines;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using SQLMultiFlowWeb.Areas.Processing.Models;

namespace SQLMultiFlowWeb.Areas.Processing.Controllers
{
    [Area("Processing")]
    [ApiController]
    [Route("/Processing/[controller]")]
    public class ScriptsServersController : ControllerBase
    {
        public SQLMultyFlowWebContext DB { get; set; }

        public ScriptsServersController(SQLMultyFlowWebContext DB)
        {
            this.DB = DB;
        }


        [HttpPost]
        public async Task<IActionResult> On_Post([FromBody] BodyRead body)
        {
            return await Task.Run(() =>
            {
                dynamic request;

                if (body.Marker == "All")
                {
                    request = (from relations in DB.TbRelations
                               join servers in DB.TbServerList on relations.ServerListId equals servers.Id
                               join markers in DB.TbMarkers on relations.MarkerId equals markers.Id
                               select new
                               {
                                   ServerDomainName = servers.ServerDomainName,
                                   DataBaseName = servers.DataBaseName
                               }).ToList();
                }
                else
                {
                    request = (from relations in DB.TbRelations
                               join servers in DB.TbServerList on relations.ServerListId equals servers.Id
                               join markers in DB.TbMarkers on relations.MarkerId equals markers.Id
                               where markers.MarkerName == body.Marker
                               select new
                               {
                                   ServerDomainName = servers.ServerDomainName,
                                   DataBaseName = servers.DataBaseName
                               }).ToList();
                }


                var jsonResult = JsonConvert.SerializeObject(request);




                HttpContext.Response.Headers["Content-Type"] = "application/json";

                return Content(jsonResult);
            });
        }
    }
}