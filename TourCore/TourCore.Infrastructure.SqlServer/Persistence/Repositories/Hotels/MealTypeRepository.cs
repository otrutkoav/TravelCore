using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions.Persistence.Hotels;
using TourCore.Domain.Hotels.Entities;

namespace TourCore.Infrastructure.SqlServer.Persistence.Repositories
{
    public class MealTypeRepository : IMealTypeRepository
    {
        private readonly TourCoreDbContext _context;

        public MealTypeRepository(TourCoreDbContext context)
        {
            _context = context;
        }

        public IQueryable<MealType> Query()
        {
            return _context.MealTypes;
        }

        public async Task<MealType> GetByIdAsync(int id, CancellationToken ct)
        {
            return await _context.MealTypes.FirstOrDefaultAsync(x => x.Id == id, ct);
        }

        public async Task<IReadOnlyCollection<MealType>> ListAsync(CancellationToken ct)
        {
            return await _context.MealTypes.ToListAsync(ct);
        }

        public async Task<bool> ExistsByCodeAsync(string code, CancellationToken ct)
        {
            return await _context.MealTypes.AnyAsync(x => x.Code == code, ct);
        }

        public async Task<bool> ExistsByCodeAsync(string code, int excludeId, CancellationToken ct)
        {
            return await _context.MealTypes.AnyAsync(
                x => x.Code == code && x.Id != excludeId,
                ct);
        }

        public async Task<bool> ExistsByGlobalCodeAsync(string globalCode, CancellationToken ct)
        {
            return await _context.MealTypes.AnyAsync(x => x.GlobalCode == globalCode, ct);
        }

        public async Task<bool> ExistsByGlobalCodeAsync(string globalCode, int excludeId, CancellationToken ct)
        {
            return await _context.MealTypes.AnyAsync(
                x => x.GlobalCode == globalCode && x.Id != excludeId,
                ct);
        }

        public Task AddAsync(MealType mealType, CancellationToken ct)
        {
            _context.MealTypes.Add(mealType);
            return Task.CompletedTask;
        }
    }
}