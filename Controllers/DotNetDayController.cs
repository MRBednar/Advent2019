using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Advent2019.DotNetCoreSolutions;

namespace Advent2019.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DotNetDayController : ControllerBase
    {
        private readonly ILogger<DotNetDayController> _logger;

        public DotNetDayController(ILogger<DotNetDayController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            var runner = new DayRunner();
            var answerString = runner.RunDay(1);
            var returnArray = new List<string>();
            returnArray.Add(answerString);
            return returnArray;
        }

    }
}
