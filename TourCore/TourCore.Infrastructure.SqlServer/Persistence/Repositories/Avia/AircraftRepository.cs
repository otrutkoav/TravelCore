using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions.Persistence.Avia;
using TourCore.Domain.Avia.Entities;
using TourCore.Infrastructure.Persistence;
using TourCore.Infrastructure.SqlServer.Persistence;

namespace TourCore.Infrastructure.SqlServer.Persistence.Repositories.Avia
{
    public class AircraftRepository : IAircraftRepository
    {
        private readonly TourCoreDbContext _context;

        public AircraftRepository(TourCoreDbContext context)
        {
            _context = context;
        }

        public IQueryable<Aircraft> Query()
        {
            return _context.Aircrafts;
        }

        public async Task<Aircraft> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Aircrafts
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<IReadOnlyCollection<Aircraft>> ListAsync(CancellationToken cancellationToken)
        {
            return await _context.Aircrafts
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> ExistsByCodeAsync(string code, CancellationToken cancellationToken)
        {
            return await _context.Aircrafts
                .AnyAsync(x => x.Code == code, cancellationToken);
        }

        public async Task<bool> ExistsByCodeAsync(
            string code,
            int excludeId,
            CancellationToken cancellationToken)
        {
            return await _context.Aircrafts
                .AnyAsync(x =>
                    x.Code == code &&
                    x.Id != excludeId,
                    cancellationToken);
        }

        public async Task<bool> ExistsByCodeValueAsync(
            string code,
            CancellationToken cancellationToken)
        {
            return await _context.Aircrafts
                .AnyAsync(x => x.Code == code, cancellationToken);
        }

        public async Task AddAsync(Aircraft entity, CancellationToken cancellationToken)
        {
            _context.Aircrafts.Add(entity);
            await Task.CompletedTask;
        }
    }
}