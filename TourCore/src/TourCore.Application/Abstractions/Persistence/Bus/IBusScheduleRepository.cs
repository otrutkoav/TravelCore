using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Common.Data;
using TourCore.Domain.Bus.Entities;

namespace TourCore.Application.Abstractions.Persistence.Bus
{
    public interface IBusScheduleRepository : IQueryableRepository<BusSchedule>
    {
        Task<BusSchedule> GetByIdAsync(int id, CancellationToken cancellationToken);

        Task<IReadOnlyCollection<BusSchedule>> ListAsync(CancellationToken cancellationToken);

        Task AddAsync(BusSchedule busSchedule, CancellationToken cancellationToken);
    }
}