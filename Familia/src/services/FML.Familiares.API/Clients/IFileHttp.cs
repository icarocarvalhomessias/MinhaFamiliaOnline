namespace FML.Familiares.API.Clients
{
    public interface IFileHttp
    {
        Task<string> ImageUrlAsync(Guid id);
        Task<HttpResponseMessage> UploadPhotoAsync(byte[] photo, string fileName);
    }
}