using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.S3;

namespace Advent2019.DotNetCoreSolutions.Days
{
    public class Day4 : BaseDay
    {
        public static List<string> dayInput;

        public override string Run()
        {
            var part1 = Part1().Result;
            return part1;
        }

        private async Task<string> Part1()
        {
            using (AmazonS3Client s3Client = new AmazonS3Client())
            {
                var getInput = new GetInputFromS3(s3Client);
                dayInput = await getInput.GetDayInput(4);
            }

            var possiblePasswords = PasswordProcessor(dayInput.First());

            return string.Format("Part 1 potential pass: {0} || Part 2 potential pass: {1}", possiblePasswords[0], possiblePasswords[1]);
        }

        private List<string> PasswordProcessor(string input)
        {
            var inputRanges = input.Split('-');
            var lowerRange = int.Parse(inputRanges[0]);
            var upperRange = int.Parse(inputRanges[1]);
            var potentialList = new List<int>();
            var part2List = new List<int>();
            var returnStrings = new List<string>();

            var currentCheck = lowerRange;

            while(currentCheck < upperRange)
            {
                var doubleDigits = false;
                var currentChars = currentCheck.ToString().ToCharArray();
                var part2Digits = false;
                var potentialDouble = 0;
                var repeatedDigitCount = 0;

                var check1 = Convert.ToInt32(char.GetNumericValue(currentChars[0]));
                var check2 = Convert.ToInt32(char.GetNumericValue(currentChars[1]));
                if (check1 > check2)
                {
                    var mod = check1 - check2;
                    currentCheck += (mod * 10000);
                    continue;
                }
                if (check1 == check2)
                {
                    doubleDigits = true;
                    potentialDouble = check2;
                    repeatedDigitCount++;
                }

                var check3 = Convert.ToInt32(char.GetNumericValue(currentChars[2]));
                if (check2 > check3)
                {
                    var mod = check2 - check3;
                    currentCheck += (mod * 1000);
                    continue;
                }
                if (check2 == check3)
                {
                    if(potentialDouble != check3)
                    {
                        part2Digits = true;
                    }

                    repeatedDigitCount++;
                    potentialDouble = check3;
                    doubleDigits = true;
                } else
                {
                    repeatedDigitCount = 0;
                }

                var check4 = Convert.ToInt32(char.GetNumericValue(currentChars[3]));
                if (check3 > check4)
                {
                    var mod = check3 - check4;
                    currentCheck += (mod * 100);
                    continue;
                }
                if (check3 == check4)
                {
                    if (repeatedDigitCount < 1)
                    {
                        part2Digits = true;
                    }

                    doubleDigits = true;
                }

                var check5 = Convert.ToInt32(char.GetNumericValue(currentChars[4]));
                if (check4 > check5)
                {
                    var mod = check4 - check5;
                    currentCheck += (mod * 10);
                    continue;
                }
                if (check4 == check5)
                {
                    if (potentialDouble != check5)
                    {
                        part2Digits = true;
                    }

                    doubleDigits = true;
                }

                var check6 = Convert.ToInt32(char.GetNumericValue(currentChars[5]));
                if (check5 > check6)
                {
                    var mod = check5 - check6;
                    currentCheck += mod;
                    continue;
                }
                if (check5 == check6)
                {

                    if (potentialDouble != check6)
                    {
                        part2Digits = true;
                    }

                    doubleDigits = true;
                }

                if(doubleDigits)
                {
                    potentialList.Add(currentCheck);
                    if (part2Digits)
                    {
                        part2List.Add(currentCheck);
                    }
                }

                currentCheck++;
                continue;
            }

            returnStrings.Add(potentialList.Count.ToString());
            returnStrings.Add(part2List.Count.ToString());

            return returnStrings;
        }
    }
}