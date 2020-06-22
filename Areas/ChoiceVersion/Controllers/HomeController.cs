using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;
using SQLMultiFlowWeb.Areas.ChoiceVersion;
using SQLMultiFlowWeb.Areas.ChoiceVersion.Models;

namespace SQLMultiFlowWeb.Areas.ChoiceVersion.Controllers
{
    [Area("ChoiceVersion")]
    public class HomeController : Controller
    {

        public SQLMultyFlowWebContext DB { get; set; }

        public HomeController(SQLMultyFlowWebContext DB)
        {
            this.DB = DB;
        }


        public IActionResult Index()
        {
            ConsolidatedModel model = new ConsolidatedModel()
            {
                Scripts = new List<ScriptNameIDVersions>(),
                Servers = new List<ServersIDNameDB>(),
                Markers = new List<MarkerIDName>()
            };

            Dictionary<string, List<decimal>> dic = new Dictionary<string, List<decimal>>();
            

            var request = (from scriptVersion in DB.TbScriptVersion
                          join scriptName in DB.TbScriptsNames on scriptVersion.ScriptId equals scriptName.Id

                          select new KeyValuePair<string, decimal>(scriptName.ScriptName, scriptVersion.Virsion)).ToArray();


            foreach (var i in request)
            {
                if(dic.ContainsKey(i.Key)==false)
                {
                    dic.Add(i.Key, new List<decimal> { i.Value });
                }
                else
                {
                    dic[i.Key].Add(i.Value);
                }
            }

            foreach (var i in dic)
            {
                int scriptsID = (from scriptVersions in DB.TbScriptVersion
                                 join scriptNames in DB.TbScriptsNames on scriptVersions.ScriptId equals scriptNames.Id

                                 where scriptNames.ScriptName == i.Key

                                 select scriptVersions.ScriptId).FirstOrDefault();

                model.Scripts.Add(new ScriptNameIDVersions
                {
                    ScriptName = i.Key,
                    Versions = i.Value,
                    ScriptID = scriptsID
                });
            }

            model.Servers = (from relations in DB.TbRelations
                             join servers in DB.TbServerList on relations.ServerListId equals servers.Id
                             join markers in DB.TbMarkers on relations.MarkerId equals markers.Id

                             select new ServersIDNameDB
                             {
                                 ID = servers.Id,
                                 ServerName = servers.ServerDomainName,
                                 DB = servers.DataBaseName,
                             }).ToList();

            model.Markers = DB.TbMarkers.Select(s => new MarkerIDName { ID = s.Id, Marker = s.MarkerName }).ToList();

            return View(model);
        }
    }
}