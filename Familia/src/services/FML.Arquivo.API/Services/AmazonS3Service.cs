using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using FML.File.API.Services.Interfaces;

namespace FML.File.API.Services
{
    public class AmazonS3Service : IAmazonS3Service
    {
        private readonly IAmazonS3 _awsS3Client;

        public AmazonS3Service()
        {
            var awsKeyID = "AKIAUYQ3B2764L3767E2";
            var awsSecretKey = "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";

            if (string.IsNullOrEmpty(awsKeyID) || string.IsNullOrEmpty(awsSecretKey))
            {
                throw new InvalidOperationException("AWS credentials are not set in the environment variables.");
            }

            var basicAwsCredentials = new BasicAWSCredentials(awsKeyID, awsSecretKey);
            var cfg = new AmazonS3Config
            {
                RegionEndpoint = Amazon.RegionEndpoint.USEast1
            };

            _awsS3Client = new AmazonS3Client(basicAwsCredentials, cfg);
        }

        public async Task<bool> SaveFileAsync(string bucket, string key, IFormFile file)
        {
            using var newMemoryStream = new MemoryStream();
            await file.CopyToAsync(newMemoryStream);

            var fileTransferUtility = new TransferUtility(_awsS3Client);

            await fileTransferUtility.UploadAsync(new TransferUtilityUploadRequest
            {
                InputStream = newMemoryStream,
                BucketName = bucket,
                Key = key,
                ContentType = file.ContentType
            });

            return true;
        }

        public async Task<string> GetFileUrlAsync(string bucket, string key)
        {
            var request = new GetPreSignedUrlRequest
            {
                BucketName = bucket,
                Key = key,
                Expires = DateTime.Now.AddHours(1)
            };
            return _awsS3Client.GetPreSignedURL(request);
        }
    }
}
