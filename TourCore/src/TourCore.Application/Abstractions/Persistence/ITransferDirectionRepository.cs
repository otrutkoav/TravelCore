using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Domain.Transfers.Entities;

namespace TourCore.Application.Abstractions.Persistence
{
    public interface ITransferDirectionRepository
    {
        Task<TransferDirection> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<IReadOnlyCollection<TransferDirection>> ListAsync(CancellationToken cancellationToken);

        Task AddAsync(TransferDirection transferDirection, CancellationToken cancellationToken);
    }
}