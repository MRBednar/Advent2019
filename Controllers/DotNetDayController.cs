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
        [HttpGet("{id}")]
        public IEnumerable<string> Get(int id)
        {
            var runner = new DayRunner();
            var answerString = runner.RunDay(id);
            var returnArray = new List<string>
            {
                answerString
            };
            return returnArray;
        }

    }
}
