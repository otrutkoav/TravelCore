using System.Collections.Generic;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions.Persistence.Hotels;
using TourCore.Domain.Hotels.Entities;

namespace TourCore.Infrastructure.SqlServer.Persistence.Repositories.Hotels
{
    public class RoomTypeRepository : IRoomTypeRepository
    {
        private readonly TourCoreDbContext _context;

        public RoomTypeRepository(TourCoreDbContext context)
        {
            _context = context;
        }

        public async Task<RoomType> GetByIdAsync(int id, CancellationToken ct)
        {
            return await _context.RoomTypes.FirstOrDefaultAsync(x => x.Id == id, ct);
        }

        public async Task<IReadOnlyCollection<RoomType>> ListAsync(CancellationToken ct)
        {
            return await _context.RoomTypes.ToListAsync(ct);
        }

        public async Task<bool> ExistsByCodeAsync(string code, CancellationToken ct)
        {
            return await _context.RoomTypes.AnyAsync(x => x.Code == code, ct);
        }

        public async Task<bool> ExistsByCodeAsync(string code, int excludeId, CancellationToken ct)
        {
            return await _context.RoomTypes.AnyAsync(
                x => x.Code == code && x.Id != excludeId,
                ct);
        }

        public Task AddAsync(RoomType roomType, CancellationToken ct)
        {
            _context.RoomTypes.Add(roomType);
            return Task.CompletedTask;
        }
    }
}