using System.Collections.Generic;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions.Persistence.Finance;
using TourCore.Domain.Finance.Entities;

namespace TourCore.Infrastructure.SqlServer.Persistence.Repositories.Finance
{
    public class RateRepository : IRateRepository
    {
        private readonly TourCoreDbContext _context;

        public RateRepository(TourCoreDbContext context)
        {
            _context = context;
        }

        public async Task<Rate> GetByIdAsync(int id, CancellationToken ct)
        {
            return await _context.Rates.FirstOrDefaultAsync(x => x.Id == id, ct);
        }

        public async Task<IReadOnlyCollection<Rate>> ListAsync(CancellationToken ct)
        {
            return await _context.Rates.ToListAsync(ct);
        }

        public async Task<bool> ExistsByCodeAsync(string code, CancellationToken ct)
        {
            return await _context.Rates.AnyAsync(x => x.Code == code, ct);
        }

        public async Task<bool> ExistsByCodeAsync(string code, int excludeId, CancellationToken ct)
        {
            return await _context.Rates.AnyAsync(
                x => x.Code == code && x.Id != excludeId,
                ct);
        }

        public async Task<bool> ExistsByIsoCodeAsync(string isoCode, CancellationToken ct)
        {
            return await _context.Rates.AnyAsync(x => x.IsoCode == isoCode, ct);
        }

        public async Task<bool> ExistsByIsoCodeAsync(string isoCode, int excludeId, CancellationToken ct)
        {
            return await _context.Rates.AnyAsync(
                x => x.IsoCode == isoCode && x.Id != excludeId,
                ct);
        }

        public Task AddAsync(Rate rate, CancellationToken ct)
        {
            _context.Rates.Add(rate);
            return Task.CompletedTask;
        }

        public async Task<bool> ExistsByCodeValueAsync(string code, CancellationToken ct)
        {
            return await _context.Rates.AnyAsync(x => x.Code == code, ct);
        }
    }
}