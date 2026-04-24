using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Domain.Bus.Entities;

namespace TourCore.Application.Abstractions.Persistence
{
    public interface IBusTransferPointRepository
    {
        Task<BusTransferPoint> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<IReadOnlyCollection<BusTransferPoint>> ListAsync(CancellationToken cancellationToken);

        Task AddAsync(BusTransferPoint busTransferPoint, CancellationToken cancellationToken);
    }
}