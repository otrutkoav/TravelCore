using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Domain.Geography.Entities;

namespace TourCore.Application.Abstractions.Persistence
{
    public interface IRegionRepository
    {
        Task<Region> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<IReadOnlyCollection<Region>> ListAsync(CancellationToken cancellationToken);

        Task<bool> ExistsAsync(int id, CancellationToken cancellationToken);

        Task<bool> ExistsByCodeAsync(string code, CancellationToken cancellationToken);
        Task<bool> ExistsByCodeAsync(string code, int excludeId, CancellationToken cancellationToken);

        Task AddAsync(Region region, CancellationToken cancellationToken);
    }
}