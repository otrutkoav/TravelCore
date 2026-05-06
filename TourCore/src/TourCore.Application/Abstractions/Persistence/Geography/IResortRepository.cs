using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Common.Data;
using TourCore.Domain.Geography.Entities;

namespace TourCore.Application.Abstractions.Persistence.Geography
{
    public interface IResortRepository : IQueryableRepository<Resort>
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

        Task UpdateAsync(Resort resort, CancellationToken cancellationToken);
    }
}