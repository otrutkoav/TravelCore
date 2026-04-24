using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Domain.Avia.Entities;

namespace TourCore.Application.Abstractions.Persistence
{
    public interface IAirlineRepository
    {
        Task<Airline> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<IReadOnlyCollection<Airline>> ListAsync(CancellationToken cancellationToken);

        Task<bool> ExistsByCodeAsync(string code, CancellationToken cancellationToken);
        Task<bool> ExistsByCodeAsync(string code, int excludeId, CancellationToken cancellationToken);

        Task<bool> ExistsByIcaoCodeAsync(string icaoCode, CancellationToken cancellationToken);
        Task<bool> ExistsByIcaoCodeAsync(string icaoCode, int excludeId, CancellationToken cancellationToken);

        Task<bool> ExistsByCodeValueAsync(string code, CancellationToken cancellationToken);

        Task AddAsync(Airline airline, CancellationToken cancellationToken);
    }
}