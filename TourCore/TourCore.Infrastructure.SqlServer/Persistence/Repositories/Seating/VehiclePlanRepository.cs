using System.Collections.Generic;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions.Persistence.Seating;
using TourCore.Domain.Seating.Entities;

namespace TourCore.Infrastructure.SqlServer.Persistence.Repositories.Seating
{
    /// <summary>
    /// Репозиторий схем транспорта.
    /// </summary>
    public class VehiclePlanRepository : IVehiclePlanRepository
    {
        private readonly TourCoreDbContext _context;

        public VehiclePlanRepository(TourCoreDbContext context)
        {
            _context = context;
        }

        public async Task<VehiclePlan> GetByIdAsync(int id, CancellationToken ct)
        {
            return await _context.VehiclePlans
                .FirstOrDefaultAsync(x => x.Id == id, ct);
        }

        public async Task<IReadOnlyCollection<VehiclePlan>> ListAsync(CancellationToken ct)
        {
            return await _context.VehiclePlans.ToListAsync(ct);
        }

        public Task AddAsync(VehiclePlan vehiclePlan, CancellationToken ct)
        {
            _context.VehiclePlans.Add(vehiclePlan);
            return Task.CompletedTask;
        }
    }
}