using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Advent2019.DotNetCoreSolutions.Days;

namespace Advent2019.DotNetCoreSolutions
{
    class DayRunner
    {
        public string RunDay(int day)
        {
            var returnData = dayArgument[day].Run();
            return returnData;
        }

        public static Dictionary<int, IDay>
            dayArgument = new Dictionary<int, IDay>
            {
                {1, new Day1() },
                {2, new Day2() },
            };
    }
}
