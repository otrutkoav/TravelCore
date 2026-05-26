using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Common.Data;
using TourCore.Domain.Seating.Entities;

namespace TourCore.Application.Abstractions.Persistence.Seating
{
    public interface IVehiclePlanRepository : IQueryableRepository<VehiclePlan>
    {
        Task<VehiclePlan> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<IReadOnlyCollection<VehiclePlan>> ListAsync(CancellationToken cancellationToken);

        Task AddAsync(VehiclePlan vehiclePlan, CancellationToken cancellationToken);
    }
}