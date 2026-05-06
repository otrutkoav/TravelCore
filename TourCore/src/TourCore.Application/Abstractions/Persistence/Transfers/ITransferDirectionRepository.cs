using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Common.Data;
using TourCore.Domain.Transfers.Entities;

namespace TourCore.Application.Abstractions.Persistence.Transfers
{
    public interface ITransferDirectionRepository : IQueryableRepository<TransferDirection>
    {
        Task<TransferDirection> GetByIdAsync(int id, CancellationToken cancellationToken);

        Task<IReadOnlyCollection<TransferDirection>> ListAsync(CancellationToken cancellationToken);

        Task AddAsync(TransferDirection transferDirection, CancellationToken cancellationToken);
    }
}