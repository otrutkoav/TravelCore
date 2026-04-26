using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Domain.Geography.Entities;

namespace TourCore.Application.Abstractions.Persistence
{
    public interface ICountryRepository
    {
        Task<Country> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<IReadOnlyCollection<Country>> ListAsync(CancellationToken cancellationToken);

        Task<bool> ExistsAsync(int id, CancellationToken cancellationToken);

        Task<bool> ExistsByCodeAsync(string code, CancellationToken cancellationToken);
        Task<bool> ExistsByCodeAsync(string code, int excludeId, CancellationToken cancellationToken);

        Task<bool> ExistsByIsoCode2Async(string isoCode2, CancellationToken cancellationToken);
        Task<bool> ExistsByIsoCode2Async(string isoCode2, int excludeId, CancellationToken cancellationToken);

        Task<bool> ExistsByIsoCode3Async(string isoCode3, CancellationToken cancellationToken);
        Task<bool> ExistsByIsoCode3Async(string isoCode3, int excludeId, CancellationToken cancellationToken);

        Task AddAsync(Country country, CancellationToken cancellationToken);
        Task UpdateAsync(Country country, CancellationToken cancellationToken);
    }
}