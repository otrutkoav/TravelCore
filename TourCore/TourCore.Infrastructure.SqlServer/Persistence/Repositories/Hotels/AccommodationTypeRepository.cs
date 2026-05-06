using System.Collections.Generic;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions.Persistence.Hotels;
using TourCore.Domain.Hotels.Entities;

namespace TourCore.Infrastructure.SqlServer.Persistence.Repositories.Hotels
{
    public class AccommodationTypeRepository : IAccommodationTypeRepository
    {
        private readonly TourCoreDbContext _context;

        public AccommodationTypeRepository(TourCoreDbContext context)
        {
            _context = context;
        }

        public async Task<AccommodationType> GetByIdAsync(int id, CancellationToken ct)
        {
            return await _context.AccommodationTypes
                .Include(x => x.MainPlacementRule.ChildAgeRanges)
                .Include(x => x.ExtraPlacementRule.ChildAgeRanges)
                .FirstOrDefaultAsync(x => x.Id == id, ct);
        }

        public async Task<IReadOnlyCollection<AccommodationType>> ListAsync(CancellationToken ct)
        {
            return await _context.AccommodationTypes
                .Include(x => x.MainPlacementRule.ChildAgeRanges)
                .Include(x => x.ExtraPlacementRule.ChildAgeRanges)
                .ToListAsync(ct);
        }

        public async Task<bool> ExistsByCodeAsync(string code, CancellationToken ct)
        {
            return await _context.AccommodationTypes
                .AnyAsync(x => x.Code == code, ct);
        }

        public async Task<bool> ExistsByCodeAsync(string code, int excludeId, CancellationToken ct)
        {
            return await _context.AccommodationTypes
                .AnyAsync(x => x.Code == code && x.Id != excludeId, ct);
        }

        public Task AddAsync(AccommodationType accommodationType, CancellationToken ct)
        {
            _context.AccommodationTypes.Add(accommodationType);
            return Task.CompletedTask;
        }
    }
}