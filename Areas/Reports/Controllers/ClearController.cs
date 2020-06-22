using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Microsoft.Extensions.Configuration;
using LinqToDB;

namespace SQLMultiFlowWeb.Areas.Reports.Controllers
{
    [Area("Reports")]
    [Authorize]
    public class ClearController : Controller
    {
        private readonly IConfiguration configuration;

        public SQLMultyFlowWebContext DB { get; set; }

        public ClearController(SQLMultyFlowWebContext DB, IConfiguration configuration)
        {
            this.DB = DB;
            this.configuration = configuration;
        }


        public IActionResult Index()
        {
            Dictionary<string, string> model = new Dictionary<string, string>();
            model.Add("Error table", nameof(DB.TbErrors));
            model.Add("Info table", nameof(DB.TbInfo));
            model.Add("Flowed table", nameof(DB.TbFlowed));

            return View(model);
        }

        [HttpPost]
        public IActionResult Clear()
        {
            var rows = new StringBuilder();

            using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("default")))
            {
                connection.Open();

                SqlCommand command;

                foreach (var i in HttpContext.Request.Form)
                {
                    if (i.Key != "__RequestVerificationToken")
                    {
                        switch (i.Value)
                        {
                            case "TbErrors":
                                command = new SqlCommand("TRUNCATE TABLE sh_Reports.tb_Errors", connection);
                                command.ExecuteNonQuery();
                                continue;

                            case "TbInfo":
                                command = new SqlCommand("TRUNCATE TABLE sh_Reports.tb_Info", connection);
                                command.ExecuteNonQuery();
                                continue;
                            case "TbFlowed":
                                command = new SqlCommand("TRUNCATE TABLE sh_Reports.tb_Flowed", connection);
                                command.ExecuteNonQuery();
                                continue;
                            default:
                                break;
                        }
                    }
                }
            }

            Response.Headers.Add("REFRESH", $"2.5;/Reports/Home/Index");
            return Content("Очищено");
        }
    }
}