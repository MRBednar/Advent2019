using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.S3;

namespace Advent2019.DotNetCoreSolutions.Days
{
    public class Day2 : BaseDay
    {
        public override string Run()
        {
            return RunDay2().Result;
        }

        public async Task<string> RunDay2 ()
        {
            var inputInts = new List<int>();

            using (AmazonS3Client s3Client = new AmazonS3Client())
            {
                var getInput = new GetInputFromS3(s3Client);
                var dayInput = await getInput.GetDayInput(2);
                var inputString = dayInput.FirstOrDefault();
                var inputList = inputString.Split(',').ToList();

                inputList.ForEach(x => inputInts.Add(int.Parse(x)));
            }

            var p1Noun = 12;
            var p1Verb = 2;
            var p1InputArray = new int[inputInts.Count];
            inputInts.CopyTo(p1InputArray);
            var p1InputList = p1InputArray.ToList();

            var part1Int = IntercodeProcessing(p1Noun, p1Verb, p1InputList);

            var p2Noun = 13;
            var p2Verb = 2;
            var p2NounInputArray = new int[inputInts.Count];
            var p2VerbInputArray = new int[inputInts.Count];
            inputInts.CopyTo(p2NounInputArray);
            var p2NounInputList = p2NounInputArray.ToList();
            var part2Int = IntercodeProcessing(p2Noun, p2Verb, p2NounInputList);

            Console.WriteLine(part2Int);

            var nounDiff = part2Int - part1Int;

            var goalOutput = 19690720;

            var goalDiff = goalOutput - part1Int;

            var nounIncNeeded = goalDiff / nounDiff;

            p2Noun = p1Noun + nounIncNeeded;

            inputInts.CopyTo(p2VerbInputArray);
            var p2VerbInputList = p2VerbInputArray.ToList();
            part2Int = IntercodeProcessing(p2Noun, p2Verb, p2VerbInputList);

            var verbDiff = goalOutput - part2Int;

            p2Verb = p1Verb + verbDiff;

            var p2Result = (p2Noun * 100) + p2Verb;

            return string.Format("Part1 Value at 0: {0}  || Part2 input result: {1}", part1Int, p2Result);
        }

        private int IntercodeProcessing(int noun, int verb, List<int> memoryList)
        {
            var runNextOpCode = true;
            var opIndex = 0;
            var returnCode = 0;
            memoryList[1] = noun;
            memoryList[2] = verb;

            while (runNextOpCode)
            {
                var opCode = memoryList[opIndex];

                switch (opCode)
                {
                    case 99:
                        runNextOpCode = false;
                        returnCode = memoryList[0];
                        break;
                    case 1:
                        var int1 = memoryList[opIndex + 1];
                        var int2 = memoryList[opIndex + 2];
                        var newIntLoc = memoryList[opIndex + 3];
                        memoryList[newIntLoc] = memoryList[int1] + memoryList[int2];
                        opIndex = opIndex + 4;
                        continue;
                    case 2:
                        var mInt1 = memoryList[opIndex + 1];
                        var mInt2 = memoryList[opIndex + 2];
                        var mNewIntLoc = memoryList[opIndex + 3];
                        memoryList[mNewIntLoc] = memoryList[mInt1] * memoryList[mInt2];
                        opIndex = opIndex + 4;
                        continue;
                    default:
                        throw new ArgumentException("Something wrong with input");
                }
            }

            return returnCode;
        }
    }
}
