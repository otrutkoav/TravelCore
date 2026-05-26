using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Common.Data;
using TourCore.Domain.Avia.Entities;

namespace TourCore.Application.Abstractions.Persistence.Avia
{
    public interface IAirClassRepository : IQueryableRepository<AirClass>
    {
        Task<AirClass> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<IReadOnlyCollection<AirClass>> ListAsync(CancellationToken cancellationToken);

        Task<bool> ExistsByCodeAsync(string code, CancellationToken cancellationToken);
        Task<bool> ExistsByCodeAsync(string code, int excludeId, CancellationToken cancellationToken);

        Task<bool> ExistsByCodeValueAsync(string code, CancellationToken cancellationToken);

        Task AddAsync(AirClass entity, CancellationToken cancellationToken);
    }
}