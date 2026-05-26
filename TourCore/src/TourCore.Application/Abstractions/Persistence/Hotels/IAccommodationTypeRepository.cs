using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Common.Data;
using TourCore.Domain.Hotels.Entities;

namespace TourCore.Application.Abstractions.Persistence.Hotels
{
    public interface IAccommodationTypeRepository : IQueryableRepository<AccommodationType>
    {
        Task<AccommodationType> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<IReadOnlyCollection<AccommodationType>> ListAsync(CancellationToken cancellationToken);

        Task<bool> ExistsByCodeAsync(string code, CancellationToken cancellationToken);
        Task<bool> ExistsByCodeAsync(string code, int excludeId, CancellationToken cancellationToken);

        Task AddAsync(AccommodationType accommodationType, CancellationToken cancellationToken);
    }
}