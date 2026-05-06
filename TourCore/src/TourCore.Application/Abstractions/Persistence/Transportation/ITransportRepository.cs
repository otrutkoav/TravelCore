using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Common.Data;
using TourCore.Domain.Transportation.Entities;

namespace TourCore.Application.Abstractions.Persistence.Transportation
{
    public interface ITransportRepository : IQueryableRepository<Transport>
    {
        Task<Transport> GetByIdAsync(int id, CancellationToken cancellationToken);

        Task<IReadOnlyCollection<Transport>> ListAsync(CancellationToken cancellationToken);

        Task AddAsync(Transport transport, CancellationToken cancellationToken);
    }
}