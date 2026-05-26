using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions.Persistence.Hotels;
using TourCore.Domain.Hotels.Entities;

namespace TourCore.Infrastructure.SqlServer.Persistence.Repositories.Hotels
{
    public class HotelCategoryRepository : IHotelCategoryRepository
    {
        private readonly TourCoreDbContext _context;

        public HotelCategoryRepository(TourCoreDbContext context)
        {
            _context = context;
        }

        public IQueryable<HotelCategory> Query()
        {
            return _context.HotelCategories;
        }

        public async Task<HotelCategory> GetByIdAsync(int id, CancellationToken ct)
        {
            return await _context.HotelCategories.FirstOrDefaultAsync(x => x.Id == id, ct);
        }

        public async Task<IReadOnlyCollection<HotelCategory>> ListAsync(CancellationToken ct)
        {
            return await _context.HotelCategories.ToListAsync(ct);
        }

        public async Task<bool> ExistsByGlobalCodeAsync(string globalCode, CancellationToken ct)
        {
            return await _context.HotelCategories.AnyAsync(x => x.GlobalCode == globalCode, ct);
        }

        public async Task<bool> ExistsByGlobalCodeAsync(string globalCode, int excludeId, CancellationToken ct)
        {
            return await _context.HotelCategories.AnyAsync(
                x => x.GlobalCode == globalCode && x.Id != excludeId,
                ct);
        }

        public Task AddAsync(HotelCategory hotelCategory, CancellationToken ct)
        {
            _context.HotelCategories.Add(hotelCategory);
            return Task.CompletedTask;
        }
    }
}