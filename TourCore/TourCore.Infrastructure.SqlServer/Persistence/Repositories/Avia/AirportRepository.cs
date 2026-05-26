using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions.Persistence.Avia;
using TourCore.Domain.Avia.Entities;

namespace TourCore.Infrastructure.SqlServer.Persistence.Repositories.Avia
{
    public class AirportRepository : IAirportRepository
    {
        private readonly TourCoreDbContext _context;

        public AirportRepository(TourCoreDbContext context)
        {
            _context = context;
        }

        public IQueryable<Airport> Query()
        {
            return _context.Airports;
        }

        public async Task<Airport> GetByIdAsync(int id, CancellationToken ct)
        {
            return await _context.Airports.FirstOrDefaultAsync(x => x.Id == id, ct);
        }

        public async Task<IReadOnlyCollection<Airport>> ListAsync(CancellationToken ct)
        {
            return await _context.Airports.ToListAsync(ct);
        }

        public async Task<bool> ExistsByCodeAsync(string code, CancellationToken ct)
        {
            return await _context.Airports.AnyAsync(x => x.Code == code, ct);
        }

        public async Task<bool> ExistsByCodeAsync(string code, int excludeId, CancellationToken ct)
        {
            return await _context.Airports.AnyAsync(
                x => x.Code == code && x.Id != excludeId,
                ct);
        }

        public async Task<bool> ExistsByIcaoCodeAsync(string icaoCode, CancellationToken ct)
        {
            return await _context.Airports.AnyAsync(x => x.IcaoCode == icaoCode, ct);
        }

        public async Task<bool> ExistsByIcaoCodeAsync(string icaoCode, int excludeId, CancellationToken ct)
        {
            return await _context.Airports.AnyAsync(
                x => x.IcaoCode == icaoCode && x.Id != excludeId,
                ct);
        }

        public async Task<bool> ExistsByCodeValueAsync(string code, CancellationToken ct)
        {
            return await _context.Airports.AnyAsync(x => x.Code == code, ct);
        }

        public Task AddAsync(Airport airport, CancellationToken ct)
        {
            _context.Airports.Add(airport);
            return Task.CompletedTask;
        }
    }
}