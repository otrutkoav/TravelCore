using System.Collections.Generic;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Domain.Geography.Entities;

namespace TourCore.Infrastructure.SqlServer.Persistence.Repositories
{
    public class CityRepository : ICityRepository
    {
        private readonly TourCoreDbContext _context;

        public CityRepository(TourCoreDbContext context)
        {
            _context = context;
        }

        public async Task<City> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Cities
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<IReadOnlyCollection<City>> ListAsync(CancellationToken cancellationToken)
        {
            return await _context.Cities
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Cities
                .AnyAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<bool> ExistsByCodeAsync(
            int countryId,
            string code,
            CancellationToken cancellationToken)
        {
            return await _context.Cities
                .AnyAsync(x => x.CountryId == countryId && x.Code == code, cancellationToken);
        }

        public async Task<bool> ExistsByCodeAsync(
            int countryId,
            string code,
            int excludeId,
            CancellationToken cancellationToken)
        {
            return await _context.Cities
                .AnyAsync(x =>
                    x.CountryId == countryId &&
                    x.Code == code &&
                    x.Id != excludeId,
                    cancellationToken);
        }

        public Task AddAsync(City city, CancellationToken cancellationToken)
        {
            _context.Cities.Add(city);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(City city, CancellationToken cancellationToken)
        {
            _context.Entry(city).State = EntityState.Modified;
            return Task.CompletedTask;
        }
    }
}