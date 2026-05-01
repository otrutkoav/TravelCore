using System.Collections.Generic;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Domain.Geography.Entities;

namespace TourCore.Infrastructure.SqlServer.Persistence.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly TourCoreDbContext _context;

        public RegionRepository(TourCoreDbContext context)
        {
            _context = context;
        }

        public async Task<Region> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Regions
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<IReadOnlyCollection<Region>> ListAsync(CancellationToken cancellationToken)
        {
            return await _context.Regions
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Regions
                .AnyAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<bool> ExistsByCodeAsync(
            int countryId,
            string code,
            CancellationToken cancellationToken)
        {
            return await _context.Regions
                .AnyAsync(x =>
                    x.CountryId == countryId &&
                    x.Code == code,
                    cancellationToken);
        }

        public async Task<bool> ExistsByCodeAsync(
            int countryId,
            string code,
            int excludeId,
            CancellationToken cancellationToken)
        {
            return await _context.Regions
                .AnyAsync(x =>
                    x.CountryId == countryId &&
                    x.Code == code &&
                    x.Id != excludeId,
                    cancellationToken);
        }

        public Task AddAsync(Region region, CancellationToken cancellationToken)
        {
            _context.Regions.Add(region);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Region region, CancellationToken cancellationToken)
        {
            _context.Entry(region).State = EntityState.Modified;
            return Task.CompletedTask;
        }
    }
}