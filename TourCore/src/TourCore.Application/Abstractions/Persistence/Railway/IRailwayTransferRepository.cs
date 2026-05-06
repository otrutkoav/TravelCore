using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Common.Data;
using TourCore.Domain.Railway.Entities;

namespace TourCore.Application.Abstractions.Persistence.Railway
{
    public interface IRailwayTransferRepository : IQueryableRepository<RailwayTransfer>
    {
        Task<RailwayTransfer> GetByIdAsync(int id, CancellationToken cancellationToken);

        Task<IReadOnlyCollection<RailwayTransfer>> ListAsync(CancellationToken cancellationToken);

        Task AddAsync(RailwayTransfer railwayTransfer, CancellationToken cancellationToken);
    }
}