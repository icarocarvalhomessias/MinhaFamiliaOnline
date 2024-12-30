using FML.Core.Data;
using FML.Familiares.API.Data.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace FML.Familiares.API.Data.Repository
{
    public class RelativeRepository : IRelativeRepository
    {
        private readonly FamiliaresContext _context;

        public RelativeRepository(FamiliaresContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<IEnumerable<Relative>> GetRelativesByFamilyId(Guid familyId)
        {
            return await _context.Relatives
            .Include(r => r.Family)
            .Include(r => r.House)
            .AsNoTracking()
            .Where(r => r.FamilyId == familyId).ToListAsync();
        }

        public async Task<IEnumerable<Relative>> GetRelativesByFatherId(Guid fatherId)
        {
            return await _context.Relatives.AsNoTracking().Where(r => r.FatherId == fatherId).ToListAsync();
        }

        public async Task<IEnumerable<Relative>> GetRelativesByHouseId(Guid houseId)
        {
            return await _context.Relatives.AsNoTracking().Where(r => r.HouseId == houseId).ToListAsync();
        }

        public async Task<IEnumerable<Relative>> GetRelativesByMotherId(Guid motherId)
        {
            return await _context.Relatives.AsNoTracking().Where(r => r.MotherId == motherId).ToListAsync();
        }

        public void AddRelative(Relative relative)
        {
            _context.Relatives.Add(relative);
        }

        public async Task AddRelatives(IEnumerable<Relative> relatives)
        {
            if (relatives == null || !relatives.Any())
            {
                throw new ArgumentException("The relatives collection is null or empty.", nameof(relatives));
            }

            await _context.Relatives
                .AddRangeAsync(relatives);
        }


        public async Task<bool> RemoveRelative(Guid relativeId)
        {
            _context.Relatives
                .Remove(new Relative { Id = relativeId });
            return await _context.SaveChangesAsync() > 0;

        }

        public async Task<bool> UpdateRelative(Relative relative)
        {
            _context.Relatives
                .Update(relative);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Relative?> GetRelativeById(Guid relativeId)
        {
            return await _context.Relatives
            .Include(r => r.Family)
            .Include(r => r.House)
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Id == relativeId);
        }



        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
