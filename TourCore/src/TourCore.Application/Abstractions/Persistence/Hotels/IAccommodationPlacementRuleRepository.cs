using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Common.Data;
using TourCore.Domain.Hotels.Entities;

namespace TourCore.Application.Abstractions.Persistence.Hotels
{
    public interface IAccommodationPlacementRuleRepository
        : IRepository<AccommodationPlacementRule>
    {
        Task<AccommodationPlacementRule> GetByIdAsync(int id, CancellationToken cancellationToken);

        Task<IReadOnlyCollection<AccommodationPlacementRule>> ListAsync(CancellationToken cancellationToken);

        Task AddAsync(AccommodationPlacementRule accommodationPlacementRule, CancellationToken cancellationToken);
    }
}
