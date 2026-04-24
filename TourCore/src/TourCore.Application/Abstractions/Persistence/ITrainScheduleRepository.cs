using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Domain.Railway.Entities;

namespace TourCore.Application.Abstractions.Persistence
{
    public interface ITrainScheduleRepository
    {
        Task<TrainSchedule> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<IReadOnlyCollection<TrainSchedule>> ListAsync(CancellationToken cancellationToken);

        Task AddAsync(TrainSchedule trainSchedule, CancellationToken cancellationToken);
    }
}