using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Common.Data;
using TourCore.Domain.Railway.Entities;

namespace TourCore.Application.Abstractions.Persistence.Railway
{
    public interface ITrainScheduleRepository : IQueryableRepository<TrainSchedule>
    {
        Task<TrainSchedule> GetByIdAsync(int id, CancellationToken cancellationToken);

        Task<IReadOnlyCollection<TrainSchedule>> ListAsync(CancellationToken cancellationToken);

        Task AddAsync(TrainSchedule trainSchedule, CancellationToken cancellationToken);
    }
}