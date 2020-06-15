using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;

namespace SQLMultiFlowWeb.Areas.Processing.Controllers
{
    [Area("Processing")]
    [Route("Processing/[controller]")]
    [ApiController]
    public class FlowController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> On_Post([FromBody]Models.Flow body)
        {
            return await Task.Run(() =>
            {
                var json = JsonConvert.SerializeObject(body);

                return Content(json);
            });
        }
    }
}
