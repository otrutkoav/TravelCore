using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Domain.Geography.Entities;

namespace TourCore.Infrastructure.SqlServer.Persistence.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly TourCoreDbContext _context;

        public CountryRepository(TourCoreDbContext context)
        {
            _context = context;
        }

        public async Task<Country> GetByIdAsync(int id, CancellationToken ct)
        {
            return await _context.Countries.FirstOrDefaultAsync(x => x.Id == id, ct);
        }

        public async Task<IReadOnlyCollection<Country>> ListAsync(CancellationToken ct)
        {
            return await _context.Countries.ToListAsync(ct);
        }

        public async Task<bool> ExistsAsync(int id, CancellationToken ct)
        {
            return await _context.Countries.AnyAsync(x => x.Id == id, ct);
        }

        public async Task<bool> ExistsByCodeAsync(string code, CancellationToken ct)
        {
            return await _context.Countries.AnyAsync(x => x.Code == code, ct);
        }

        public async Task<bool> ExistsByCodeAsync(string code, int excludeId, CancellationToken ct)
        {
            return await _context.Countries.AnyAsync(x => x.Code == code && x.Id != excludeId, ct);
        }

        public async Task<bool> ExistsByIsoCode2Async(string isoCode2, CancellationToken ct)
        {
            return await _context.Countries.AnyAsync(x => x.IsoCode2 == isoCode2, ct);
        }

        public async Task<bool> ExistsByIsoCode2Async(string isoCode2, int excludeId, CancellationToken ct)
        {
            return await _context.Countries.AnyAsync(x => x.IsoCode2 == isoCode2 && x.Id != excludeId, ct);
        }

        public async Task<bool> ExistsByIsoCode3Async(string isoCode3, CancellationToken ct)
        {
            return await _context.Countries.AnyAsync(x => x.IsoCode3 == isoCode3, ct);
        }

        public async Task<bool> ExistsByIsoCode3Async(string isoCode3, int excludeId, CancellationToken ct)
        {
            return await _context.Countries.AnyAsync(x => x.IsoCode3 == isoCode3 && x.Id != excludeId, ct);
        }

        public Task AddAsync(Country country, CancellationToken ct)
        {
            _context.Countries.Add(country);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Country country, CancellationToken ct)
        {
            _context.Entry(country).State = EntityState.Modified;
            return Task.CompletedTask;
        }
    }
}