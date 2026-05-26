using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions.Persistence.Hotels;
using TourCore.Domain.Hotels.Entities;

namespace TourCore.Infrastructure.SqlServer.Persistence.Repositories
{
    public class RoomCategoryRepository : IRoomCategoryRepository
    {
        private readonly TourCoreDbContext _context;

        public RoomCategoryRepository(TourCoreDbContext context)
        {
            _context = context;
        }

        public IQueryable<RoomCategory> Query()
        {
            return _context.RoomCategories;
        }

        public async Task<RoomCategory> GetByIdAsync(int id, CancellationToken ct)
        {
            return await _context.RoomCategories.FirstOrDefaultAsync(x => x.Id == id, ct);
        }

        public async Task<IReadOnlyCollection<RoomCategory>> ListAsync(CancellationToken ct)
        {
            return await _context.RoomCategories.ToListAsync(ct);
        }

        public async Task<bool> ExistsByCodeAsync(string code, CancellationToken ct)
        {
            return await _context.RoomCategories.AnyAsync(x => x.Code == code, ct);
        }

        public async Task<bool> ExistsByCodeAsync(string code, int excludeId, CancellationToken ct)
        {
            return await _context.RoomCategories.AnyAsync(
                x => x.Code == code && x.Id != excludeId,
                ct);
        }

        public Task AddAsync(RoomCategory roomCategory, CancellationToken ct)
        {
            _context.RoomCategories.Add(roomCategory);
            return Task.CompletedTask;
        }
    }
}