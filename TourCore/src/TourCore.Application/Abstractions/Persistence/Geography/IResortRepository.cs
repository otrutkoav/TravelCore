using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Domain.Geography.Entities;

namespace TourCore.Application.Abstractions.Persistence.Geography
{
    public interface IResortRepository
    {
        Task<Resort> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<IReadOnlyCollection<Resort>> ListAsync(CancellationToken cancellationToken);

        Task<bool> ExistsByNameAsync(
            int countryId,
            string name,
            CancellationToken cancellationToken);

        Task<bool> ExistsByNameExceptIdAsync(
            int id,
            int countryId,
            string name,
            CancellationToken cancellationToken);

        Task AddAsync(Resort resort, CancellationToken cancellationToken);
    }
}