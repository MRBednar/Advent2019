﻿using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;

namespace Advent2019.DotNetCoreSolutions
{
    public class GetInputFromS3
    {
        IAmazonS3 S3Client { get; set; }

        public GetInputFromS3(IAmazonS3 s3Client)
        {
            this.S3Client = s3Client;
        }

        public async Task<List<string>> GetDayInput(int day)
        {
            List<string> results = new List<string>();

            GetObjectRequest awsRequest = new GetObjectRequest
            {
                BucketName = "advent2019bednar",
                Key = "day1Input.txt"
            };

            using (GetObjectResponse s3Response = await S3Client.GetObjectAsync(awsRequest))
            using (Stream responseStream = s3Response.ResponseStream)
            using (StreamReader s3Reader = new StreamReader(responseStream))
            {
                while (s3Reader.Peek() >= 0)
                {
                    results.Add(s3Reader.ReadLine());
                }
            }

            return results;
        }
    }
}