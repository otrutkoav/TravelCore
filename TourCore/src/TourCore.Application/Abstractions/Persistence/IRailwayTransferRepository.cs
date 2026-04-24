using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Domain.Railway.Entities;

namespace TourCore.Application.Abstractions.Persistence
{
    public interface IRailwayTransferRepository
    {
        Task<RailwayTransfer> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<IReadOnlyCollection<RailwayTransfer>> ListAsync(CancellationToken cancellationToken);

        Task AddAsync(RailwayTransfer railwayTransfer, CancellationToken cancellationToken);
    }
}