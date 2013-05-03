using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;



namespace FileMergeTest
{
    class getfile
    {
        string awsKey = "AKIAIMQI3XVCBELZLYQA";
        string awsSecret = "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";
        public void S3put()
        {
            string toWhichBucketName = "kspmerge";
            var client = Amazon.AWSClientFactory.CreateAmazonS3Client(awsKey, awsSecret);
            PutObjectRequest request = new PutObjectRequest();
            request.WithFilePath( Environment.CurrentDirectory + "\\merged\\persistent.sfs");
            request.WithBucketName(toWhichBucketName);
            client.PutObject(request);
        }

        public void S3get()
        {
            string key = "persistent.sfs";
            string bucketname = "kspmerge";

            var client = Amazon.AWSClientFactory.CreateAmazonS3Client(awsKey, awsSecret);
            GetObjectRequest request = new GetObjectRequest();
            request.WithBucketName(bucketname);
            request.WithKey(key);
            GetObjectResponse response = client.GetObject(request);
            response.WriteResponseStreamToFile(Environment.CurrentDirectory +"\\tmp\\persistent.sfs");
        }

       
    }
}
