using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Common.Data;
using TourCore.Domain.Bus.Entities;

namespace TourCore.Application.Abstractions.Persistence.Bus
{
    public interface IBusTransferPointRepository : IQueryableRepository<BusTransferPoint>
    {
        Task<BusTransferPoint> GetByIdAsync(int id, CancellationToken cancellationToken);

        Task<IReadOnlyCollection<BusTransferPoint>> ListAsync(CancellationToken cancellationToken);

        Task AddAsync(BusTransferPoint busTransferPoint, CancellationToken cancellationToken);
    }
}