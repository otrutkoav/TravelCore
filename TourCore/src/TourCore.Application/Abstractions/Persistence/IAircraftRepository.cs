using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Domain.Avia.Entities;

namespace TourCore.Application.Abstractions.Persistence
{
    public interface IAircraftRepository
    {
        Task<Aircraft> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<IReadOnlyCollection<Aircraft>> ListAsync(CancellationToken cancellationToken);

        Task<bool> ExistsByCodeAsync(string code, CancellationToken cancellationToken);
        Task<bool> ExistsByCodeAsync(string code, int excludeId, CancellationToken cancellationToken);

        Task<bool> ExistsByCodeValueAsync(string code, CancellationToken cancellationToken);

        Task AddAsync(Aircraft aircraft, CancellationToken cancellationToken);
    }
}