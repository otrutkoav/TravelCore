using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Domain.Transfers.Entities;

namespace TourCore.Application.Abstractions.Persistence
{
    public interface ITransferRepository
    {
        Task<Transfer> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<IReadOnlyCollection<Transfer>> ListAsync(CancellationToken cancellationToken);

        Task AddAsync(Transfer transfer, CancellationToken cancellationToken);
    }
}