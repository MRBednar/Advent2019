using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.S3;

namespace Advent2019.DotNetCoreSolutions.Days
{
    public class Day3 : BaseDay
    {
        public static List<string> dayInput;

        public override string Run()
        {
            var part1 = Part1().Result;
            return part1;
        }

        private async Task<string> Part1 ()
        {
            using (AmazonS3Client s3Client = new AmazonS3Client())
            {
                var getInput = new GetInputFromS3(s3Client);
                dayInput = await getInput.GetDayInput(3);
            }

            var matchLocations = new List<(int x, int y, int distance, int steps)>();

            var wireADict = WirePath(dayInput[0]);
            var wireBDict = WirePath(dayInput[1]);
            var wireALocations = wireADict.Keys.ToList();
            var wireBLocations = wireBDict.Keys.ToList();

            var intersections = wireALocations.Intersect(wireBLocations);

            foreach(var intersect in intersections)
            {
                if (intersect.Item1 !=0 && intersect.Item2!=0)
                {
                    wireADict.TryGetValue(intersect, out var aSteps);
                    wireBDict.TryGetValue(intersect, out var bSteps);
                    var steps = aSteps + bSteps;
                    var dist = Math.Abs(intersect.Item1) + Math.Abs(intersect.Item2);
                    matchLocations.Add((intersect.Item1, intersect.Item2, dist, steps));
                }
            }

            var minDist = matchLocations.OrderBy(d => d.distance).ToList();
            var minSteps = matchLocations.OrderBy(s => s.steps).ToList();
            Console.WriteLine("min Dist: {0}", minDist.First().distance);
            Console.WriteLine("min Steps: {0}", minSteps.First().steps);
            return String.Format("min Dist: {0}  || min Steps: {1}", minDist.First().distance, minSteps.First().steps);
        }

        private Dictionary<(int, int), int> WirePath (string wireDirections)
        {
            var wireLineMove = wireDirections.Split(',');
            var locationDict = new Dictionary<(int, int), int>
            {
                { (0, 0), 0 }
            };
            var steps = 0;
            foreach(var move in wireLineMove)
            {
                var currentX = locationDict.Last().Key.Item1;
                var currentY = locationDict.Last().Key.Item2;
                var direction = move.Substring(0, 1);
                var distance = int.Parse(move.Substring(1));

                switch (direction)
                {
                    case "U":
                        int upMoves = 0;
                        while (upMoves < distance)
                        {
                            currentY += 1;
                            steps++;
                            locationDict.TryAdd((currentX, currentY), steps);
                            upMoves++;
                        }
                        continue;
                    case "D":
                        int downMoves = 0;
                        while (downMoves < distance)
                        {
                            currentY -= 1;
                            steps++;
                            locationDict.TryAdd((currentX, currentY), steps);
                            downMoves++;
                        }
                        continue;
                    case "R":
                        int rightMoves = 0;
                        while (rightMoves < distance)
                        {
                            currentX += 1;
                            steps++;
                            locationDict.TryAdd((currentX, currentY), steps);
                            rightMoves++;
                        }
                        continue;
                    case "L":
                        int leftMoves = 0;
                        while (leftMoves < distance)
                        {
                            currentX -= 1;
                            steps++;
                            locationDict.TryAdd((currentX, currentY), steps);
                            leftMoves++;
                        }
                        continue;
                    default:
                        throw new Exception();
                }
            }

            return locationDict;
        }
    }
}
