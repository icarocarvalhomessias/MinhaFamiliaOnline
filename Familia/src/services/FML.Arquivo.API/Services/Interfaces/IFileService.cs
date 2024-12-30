using FML.File.API.Data.Entities;

namespace FML.File.API.Services.Interfaces
{
    public interface IFileService
    {
        Task<Arquivo?> GetFileAsync(Guid id);
        Task<Guid> SaveFileAsync(IFormFile file);
    }
}
