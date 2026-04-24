using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Domain.Geography.Entities;

namespace TourCore.Application.Abstractions.Persistence
{
    public interface ICityRepository
    {
        Task<City> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<IReadOnlyCollection<City>> ListAsync(CancellationToken cancellationToken);

        Task<bool> ExistsByCodeAsync(string code, CancellationToken cancellationToken);
        Task<bool> ExistsByCodeAsync(string code, int excludeId, CancellationToken cancellationToken);

        Task AddAsync(City city, CancellationToken cancellationToken);
    }
}