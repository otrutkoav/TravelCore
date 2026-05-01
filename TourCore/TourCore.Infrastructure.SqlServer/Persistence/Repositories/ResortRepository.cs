using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Domain.Geography.Entities;

namespace TourCore.Infrastructure.SqlServer.Persistence.Repositories
{
    public class ResortRepository : IResortRepository
    {
        private readonly TourCoreDbContext _context;

        public ResortRepository(TourCoreDbContext context)
        {
            _context = context;
        }

        public async Task<Resort> GetByIdAsync(int id, CancellationToken ct)
        {
            return await _context.Resorts.FirstOrDefaultAsync(x => x.Id == id, ct);
        }

        public async Task<IReadOnlyCollection<Resort>> ListAsync(CancellationToken ct)
        {
            return await _context.Resorts.ToListAsync(ct);
        }

        public async Task<bool> ExistsByNameAsync(
            int countryId,
            string name,
            CancellationToken ct)
        {
            return await _context.Resorts.AnyAsync(
                x => x.CountryId == countryId && x.Name == name,
                ct);
        }

        public async Task<bool> ExistsByNameExceptIdAsync(
            int id,
            int countryId,
            string name,
            CancellationToken ct)
        {
            return await _context.Resorts.AnyAsync(
                x => x.Id != id &&
                     x.CountryId == countryId &&
                     x.Name == name,
                ct);
        }

        public Task AddAsync(Resort resort, CancellationToken ct)
        {
            _context.Resorts.Add(resort);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Resort resort, CancellationToken ct)
        {
            _context.Entry(resort).State = EntityState.Modified;
            return Task.CompletedTask;
        }
    }
}