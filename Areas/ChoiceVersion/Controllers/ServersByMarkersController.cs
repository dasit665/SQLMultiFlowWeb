using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;

namespace SQLMultiFlowWeb.Areas.ChoiceVersion.Controllers
{
    [Area("ChoiceVersion")]
    [Route("ChoiceVersion/[controller]")]
    [ApiController]
    public class ServersByMarkersController : ControllerBase
    {
        public SQLMultyFlowWebContext DB { get; set; }

        public ServersByMarkersController(SQLMultyFlowWebContext DB)
        {
            this.DB = DB;
        }

        [HttpPost]
        public async Task<IActionResult> On_Post([FromBody] Models.BodyServersByMarkers body)
        {
            return await Task.Run(() =>
            {
                dynamic requre;

                if (body.Marker != "All")
                {
                    requre = (from relations in DB.TbRelations
                              join markers in DB.TbMarkers on relations.MarkerId equals markers.Id
                              join servers in DB.TbServerList on relations.ServerListId equals servers.Id

                              where markers.MarkerName == body.Marker

                              select new
                              {
                                  ID = servers.Id,
                                  ServerDomainName = servers.ServerDomainName,
                                  DataBaseName = servers.DataBaseName
                              }).ToList().OrderBy(o => o.ID);
                }
                else
                {
                    requre = DB.TbServerList.Select(s => new { ID = s.Id, ServerDomainName = s.ServerDomainName, DataBaseName = s.DataBaseName }).ToList().OrderBy(o => o.ID);
                }



                HttpContext.Response.ContentType = "application/json";
                return Content(JsonConvert.SerializeObject(requre));
            });
        }

    }
}
