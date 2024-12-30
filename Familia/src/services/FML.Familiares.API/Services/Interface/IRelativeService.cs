using FML.Familiares.API.Models;

namespace FML.Familiares.API.Services.Interface
{
    public interface IRelativeService
    {
        Task<IEnumerable<Relative>> GetRelatives();
        Task<Guid> Add();
        Task<bool> Update(UpdateRelativeModel relative);
        Task<bool> AddRelative(Relative relative);
        Task<bool> RemoveRelative(Guid relativeId);
        Task<Relative> GetRelativeById(Guid relativeId);
    }
}
