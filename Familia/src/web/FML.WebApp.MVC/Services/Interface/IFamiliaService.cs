using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace FML.WebApp.MVC.Services.Interface
{
    public interface IFamiliaService
    {
        Task<List<Family>> GetFamilies();
        Task<List<House>> GetHousesByFamilyId(Guid familyId);
        Task<List<Relative>> GetRelatives();
        Task<Relative> GetRelativeById(Guid relativeId);
        Task AddRelative(Relative relative);
        Task UpdateRelative(Relative relative, Stream? fotoFile, string? fileName);
        Task RemoveRelative(Guid relativeId);
    }
}
