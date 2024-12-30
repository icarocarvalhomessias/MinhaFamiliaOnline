using Amazon.Runtime;

namespace FML.File.API.Services.Interfaces
{
    public interface IAmazonS3Service
    {
        Task<bool> SaveFileAsync(string bucket, string key, IFormFile file);
        Task<string> GetFileUrlAsync(string bucket, string key);
    }
}