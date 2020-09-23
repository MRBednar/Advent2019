using Amazon.S3;
using Advent2019.DotNetCoreSolutions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Advent2019.DotNetCoreSolutions.Days
{
    public class Day1 : BaseDay
    {
        public static List<string> dayInput;

        public override string Run()
        {
            return FullDay1().Result;
        }

        private async Task<string> FullDay1 ()
        {
            var part1Result = 0;
            var part2Running = 0;

            using(AmazonS3Client s3Client = new AmazonS3Client())
            {
                var getInput = new GetInputFromS3(s3Client);
                dayInput = await getInput.GetDayInput(1);
            }

            foreach (string dayLine in dayInput)
            {
                if(int.TryParse(dayLine, out var moduleMass))
                {
                    var moduleFuel = FuelCalculator(moduleMass);
                    part1Result += moduleFuel;

                    var additionalWeight = moduleFuel;
                    var additionalFuel = 0;

                    while (additionalWeight > 0)
                    {
                        additionalWeight = FuelCalculator(additionalWeight);
                        additionalFuel += additionalWeight;
                    }

                    part2Running += additionalFuel;
                }
            }

            var part2Result = part1Result + part2Running;

            var resultString = string.Format("Part 1 Result: {0}     || Part 2 Result: {1}", part1Result, part2Result);
            return resultString;
        }

        private int FuelCalculator (int mass)
        {
            int returnFuel;
            var neededFuel = (mass / 3) - 2;
            returnFuel = neededFuel > 0 ? neededFuel : 0;
            return returnFuel;
        }
    }
}
