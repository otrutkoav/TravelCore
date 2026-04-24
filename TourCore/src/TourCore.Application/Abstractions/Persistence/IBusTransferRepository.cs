using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Domain.Bus.Entities;

namespace TourCore.Application.Abstractions.Persistence
{
    public interface IBusTransferRepository
    {
        Task<BusTransfer> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<IReadOnlyCollection<BusTransfer>> ListAsync(CancellationToken cancellationToken);

        Task AddAsync(BusTransfer busTransfer, CancellationToken cancellationToken);
    }
}