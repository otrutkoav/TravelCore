using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Common.Data;
using TourCore.Domain.Avia.Entities;

namespace TourCore.Application.Abstractions.Persistence.Avia
{
    public interface IAirportRepository : IQueryableRepository<Airport>
    {
        Task<Airport> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<IReadOnlyCollection<Airport>> ListAsync(CancellationToken cancellationToken);

        Task<bool> ExistsByCodeAsync(string code, CancellationToken cancellationToken);
        Task<bool> ExistsByCodeAsync(string code, int excludeId, CancellationToken cancellationToken);

        Task<bool> ExistsByIcaoCodeAsync(string icaoCode, CancellationToken cancellationToken);
        Task<bool> ExistsByIcaoCodeAsync(string icaoCode, int excludeId, CancellationToken cancellationToken);

        Task<bool> ExistsByCodeValueAsync(string code, CancellationToken cancellationToken);

        Task AddAsync(Airport airport, CancellationToken cancellationToken);
    }
}