using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SQLMultiFlowWeb.Areas.Reprocessing.Models;

using Newtonsoft.Json;
namespace SQLMultiFlowWeb.Areas.Reprocessing.Controllers
{
    [Area("Reprocessing")]
    [Route("Reprocessing/[controller]")]
    [ApiController]
    public class ReprocessingController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult>On_Post([FromBody]ReprocessingBody body)
        {
            return await Task.Run(() =>
            {
                HttpContext.Response.Headers["Content-Type"] = "application/json";
                return Content(JsonConvert.SerializeObject(body));
            });
        }
    }
}
