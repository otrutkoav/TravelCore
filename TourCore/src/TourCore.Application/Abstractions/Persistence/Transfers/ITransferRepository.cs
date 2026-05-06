using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Common.Data;
using TourCore.Domain.Transfers.Entities;

namespace TourCore.Application.Abstractions.Persistence.Transfers
{
    public interface ITransferRepository : IQueryableRepository<Transfer>
    {
        Task<Transfer> GetByIdAsync(int id, CancellationToken cancellationToken);

        Task<IReadOnlyCollection<Transfer>> ListAsync(CancellationToken cancellationToken);

        Task AddAsync(Transfer transfer, CancellationToken cancellationToken);
    }
}