using System.Collections.Generic;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions.Persistence.Avia;
using TourCore.Domain.Avia.Entities;

namespace TourCore.Infrastructure.SqlServer.Persistence.Repositories.Avia
{
    public class AirClassRepository : IAirClassRepository
    {
        private readonly TourCoreDbContext _context;

        public AirClassRepository(TourCoreDbContext context)
        {
            _context = context;
        }

        public async Task<AirClass> GetByIdAsync(int id, CancellationToken ct)
        {
            return await _context.AirClasses.FirstOrDefaultAsync(x => x.Id == id, ct);
        }

        public async Task<IReadOnlyCollection<AirClass>> ListAsync(CancellationToken ct)
        {
            return await _context.AirClasses.ToListAsync(ct);
        }

        public async Task<bool> ExistsByCodeAsync(string code, CancellationToken ct)
        {
            return await _context.AirClasses.AnyAsync(x => x.Code == code, ct);
        }

        public async Task<bool> ExistsByCodeAsync(string code, int excludeId, CancellationToken ct)
        {
            return await _context.AirClasses.AnyAsync(
                x => x.Code == code && x.Id != excludeId,
                ct);
        }

        public async Task<bool> ExistsByCodeValueAsync(string code, CancellationToken ct)
        {
            return await _context.AirClasses.AnyAsync(x => x.Code == code, ct);
        }

        public Task AddAsync(AirClass entity, CancellationToken ct)
        {
            _context.AirClasses.Add(entity);
            return Task.CompletedTask;
        }
    }
}