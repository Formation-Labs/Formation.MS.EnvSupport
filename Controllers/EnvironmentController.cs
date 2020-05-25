using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Formation.MS.EnvSupport.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Formation.MS.EnvSupport.Extensions;

namespace Formation.MS.EnvSupport.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EnvironmentController : ControllerBase
    {
        private readonly ILogger<EnvironmentController> _logger;
        private readonly DataTable Environments = new DataTable();
        private readonly string sr = new StreamReader(@"Resources\Environments.json").ReadToEnd()
        .Replace("\n","").Replace("\r","").Replace(" ","");

        public EnvironmentController(ILogger<EnvironmentController> logger)
        {
            _logger = logger;
            Environments.Columns.Add("ID");
            Environments.Columns.Add("Application");
            Environments.Columns.Add("OperatingSystem");
            Environments.Columns.Add("Browser");
            Environments.Columns.Add("BrowserVersion");
            Environments.Columns.Add("WebServer");
            Environments.Columns.Add("WebServerVersion");
            Environments.Columns.Add("WordPressVersion");
            Environments.Columns.Add("PHPVersion");
            Environments.Columns.Add("MySQlVersion");
            Environments.Columns.Add("Enabled");

            var env = JsonSerializer.Deserialize<List<Dictionary<string,object>>>(sr);

            foreach (Dictionary<string,object> e in env)
            {
                Environments.Rows.Add(e.Values.ToArray());
            }
        }

        [HttpGet]
        public IEnumerable<Dto.Environment> Get([FromQuery]Dictionary<string,string> query)
        {
            string q = string.Join(" and ", query.ToArray()).Replace("[","").Replace("]","'").Replace(", ", "='");
            var clonedDT = Environments.Clone();

            if (!string.IsNullOrEmpty(q))
            {
                foreach (var row in Environments.Select(q)) {clonedDT.ImportRow(row); }
            }
            else { clonedDT = Environments;}

            List<Dto.Environment> envs = new List<Dto.Environment>();
            foreach (DataRow r in clonedDT.Rows)
            {
                envs.Add(r.ToObject<Dto.Environment>());
            }
            return envs.ToArray();
        }

        [HttpPost]
        public void Post([FromQuery]Dictionary<string,string> query)
        {
        }
    }
}
