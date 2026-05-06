using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Common.Data;
using TourCore.Domain.Bus.Entities;

namespace TourCore.Application.Abstractions.Persistence.Bus
{
    public interface IBusTransferRepository : IQueryableRepository<BusTransfer>
    {
        Task<BusTransfer> GetByIdAsync(int id, CancellationToken cancellationToken);

        Task<IReadOnlyCollection<BusTransfer>> ListAsync(CancellationToken cancellationToken);

        Task AddAsync(BusTransfer busTransfer, CancellationToken cancellationToken);
    }
}