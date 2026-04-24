using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Domain.Bus.Entities;

namespace TourCore.Application.Abstractions.Persistence
{
    public interface IBusScheduleRepository
    {
        Task<BusSchedule> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<IReadOnlyCollection<BusSchedule>> ListAsync(CancellationToken cancellationToken);

        Task AddAsync(BusSchedule busSchedule, CancellationToken cancellationToken);
    }
}