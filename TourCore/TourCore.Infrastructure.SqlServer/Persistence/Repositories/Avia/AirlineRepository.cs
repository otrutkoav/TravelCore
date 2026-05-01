using System.Collections.Generic;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions.Persistence.Avia;
using TourCore.Domain.Avia.Entities;

namespace TourCore.Infrastructure.SqlServer.Persistence.Repositories.Avia
{
    public class AirlineRepository : IAirlineRepository
    {
        private readonly TourCoreDbContext _context;

        public AirlineRepository(TourCoreDbContext context)
        {
            _context = context;
        }

        public async Task<Airline> GetByIdAsync(int id, CancellationToken ct)
        {
            return await _context.Airlines.FirstOrDefaultAsync(x => x.Id == id, ct);
        }

        public async Task<IReadOnlyCollection<Airline>> ListAsync(CancellationToken ct)
        {
            return await _context.Airlines.ToListAsync(ct);
        }

        public async Task<bool> ExistsByCodeAsync(string code, CancellationToken ct)
        {
            return await _context.Airlines.AnyAsync(x => x.Code == code, ct);
        }

        public async Task<bool> ExistsByCodeAsync(string code, int excludeId, CancellationToken ct)
        {
            return await _context.Airlines.AnyAsync(
                x => x.Code == code && x.Id != excludeId,
                ct);
        }

        public async Task<bool> ExistsByIcaoCodeAsync(string icaoCode, CancellationToken ct)
        {
            return await _context.Airlines.AnyAsync(x => x.IcaoCode == icaoCode, ct);
        }

        public async Task<bool> ExistsByIcaoCodeAsync(string icaoCode, int excludeId, CancellationToken ct)
        {
            return await _context.Airlines.AnyAsync(
                x => x.IcaoCode == icaoCode && x.Id != excludeId,
                ct);
        }

        public async Task<bool> ExistsByCodeValueAsync(string code, CancellationToken ct)
        {
            return await _context.Airlines.AnyAsync(x => x.Code == code, ct);
        }

        public Task AddAsync(Airline airline, CancellationToken ct)
        {
            _context.Airlines.Add(airline);
            return Task.CompletedTask;
        }
    }
}