using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Domain.Transportation.Entities;

namespace TourCore.Application.Abstractions.Persistence
{
    public interface ITransportRepository
    {
        Task<Transport> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<IReadOnlyCollection<Transport>> ListAsync(CancellationToken cancellationToken);

        Task AddAsync(Transport transport, CancellationToken cancellationToken);
    }
}