using Amazon.Runtime.SharedInterfaces;
using Amazon;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace Advent2019.DotNetCoreSolutions.Days
{
    public abstract class BaseDay : IDay
    {
        public abstract string Run();
    }
}
