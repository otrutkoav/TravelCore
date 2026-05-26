using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions.Persistence.Hotels;
using TourCore.Domain.Hotels.Entities;

namespace TourCore.Infrastructure.SqlServer.Persistence.Repositories.Hotels
{
    public class HotelRepository : IHotelRepository
    {
        private readonly TourCoreDbContext _context;

        public HotelRepository(TourCoreDbContext context)
        {
            _context = context;
        }

        public IQueryable<Hotel> Query()
        {
            return _context.Hotels;
        }

        public async Task<Hotel> GetByIdAsync(int id, CancellationToken ct)
        {
            return await _context.Hotels.FirstOrDefaultAsync(x => x.Id == id, ct);
        }

        public async Task<IReadOnlyCollection<Hotel>> ListAsync(CancellationToken ct)
        {
            return await _context.Hotels.ToListAsync(ct);
        }

        public async Task<bool> ExistsByCodeAsync(string code, CancellationToken ct)
        {
            return await _context.Hotels.AnyAsync(x => x.Code == code, ct);
        }

        public async Task<bool> ExistsByCodeAsync(string code, int excludeId, CancellationToken ct)
        {
            return await _context.Hotels.AnyAsync(x => x.Code == code && x.Id != excludeId, ct);
        }

        public Task AddAsync(Hotel hotel, CancellationToken ct)
        {
            _context.Hotels.Add(hotel);
            return Task.CompletedTask;
        }
    }
}