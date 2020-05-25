using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Formation.MS.EnvSupport.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Formation.MS.EnvSupport.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EnvironmentController : ControllerBase
    {
        private readonly ILogger<EnvironmentController> _logger;

        public EnvironmentController(ILogger<EnvironmentController> logger)
        {
            _logger = logger;
            var sr = new StreamReader(@"Resources\Environments.json").ReadToEnd();
            var environments = JsonSerializer.Deserialize<EnvironmentsList>(sr);
        }

        [HttpGet]
        public IEnumerable<Dto.Environment> Get([FromQuery]Dictionary<string,string> query)
        {
            var sr = new StreamReader(@"Resources\Environments.json").ReadToEnd();
            var environments = JsonSerializer.Deserialize<EnvironmentsList>(sr);
            return environments.Environments.ToArray();

            //if (query.Equals(null)) return environments.Environments.ToArray();
        }

        [HttpPost]
        public void Post([FromQuery]Dictionary<string,string> query)
        {
            
        }
    }
}
