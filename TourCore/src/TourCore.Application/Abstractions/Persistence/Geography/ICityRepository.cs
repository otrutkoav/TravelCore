using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Common.Data;
using TourCore.Domain.Geography.Entities;

namespace TourCore.Application.Abstractions.Persistence.Geography
{
    public interface ICityRepository : IQueryableRepository<City>
    {
        Task<City> GetByIdAsync(int id, CancellationToken cancellationToken);

        Task<IReadOnlyCollection<City>> ListAsync(CancellationToken cancellationToken);

        Task<bool> ExistsAsync(int id, CancellationToken cancellationToken);

        Task<bool> ExistsByCodeAsync(
            int countryId,
            string code,
            CancellationToken cancellationToken);

        Task<bool> ExistsByCodeAsync(
            int countryId,
            string code,
            int excludeId,
            CancellationToken cancellationToken);

        Task AddAsync(City city, CancellationToken cancellationToken);

        Task UpdateAsync(City city, CancellationToken cancellationToken);
    }
}