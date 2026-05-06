using System.Collections.Generic;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions.Persistence.Hotels;
using TourCore.Domain.Hotels.Entities;

namespace TourCore.Infrastructure.SqlServer.Persistence.Repositories.Hotels
{
    public class AccommodationPlacementRuleRepository : IAccommodationPlacementRuleRepository
    {
        private readonly TourCoreDbContext _context;

        public AccommodationPlacementRuleRepository(TourCoreDbContext context)
        {
            _context = context;
        }

        public async Task<AccommodationPlacementRule> GetByIdAsync(int id, CancellationToken ct)
        {
            return await _context.AccommodationPlacementRules
                .Include(x => x.ChildAgeRanges)
                .FirstOrDefaultAsync(x => x.Id == id, ct);
        }

        public async Task<IReadOnlyCollection<AccommodationPlacementRule>> ListAsync(CancellationToken ct)
        {
            return await _context.AccommodationPlacementRules
                .Include(x => x.ChildAgeRanges)
                .ToListAsync(ct);
        }

        public Task AddAsync(AccommodationPlacementRule accommodationPlacementRule, CancellationToken ct)
        {
            _context.AccommodationPlacementRules.Add(accommodationPlacementRule);
            return Task.CompletedTask;
        }
    }
}