using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Domain.Seating.Entities;

namespace TourCore.Application.Abstractions.Persistence
{
    public interface IVehiclePlanRepository
    {
        Task<VehiclePlan> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<IReadOnlyCollection<VehiclePlan>> ListAsync(CancellationToken cancellationToken);

        Task AddAsync(VehiclePlan vehiclePlan, CancellationToken cancellationToken);
    }
}