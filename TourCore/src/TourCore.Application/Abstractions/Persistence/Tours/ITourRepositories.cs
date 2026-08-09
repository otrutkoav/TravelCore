using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Common.Data;
using TourCore.Domain.Tours;

namespace TourCore.Application.Abstractions.Persistence.Tours
{
    public interface ITourTypeRepository : IQueryableRepository<TourType>
    {
        Task<TourType> GetByIdAsync(int id, CancellationToken ct);
        Task<IReadOnlyCollection<TourType>> GetByIdsAsync(
            IEnumerable<int> ids,
            CancellationToken ct
        );
        Task<bool> ExistsByNameAsync(string name, int? excludeId, CancellationToken ct);
        Task<bool> HasChildrenAsync(int id, CancellationToken ct);
        Task<bool> IsUsedAsync(int id, CancellationToken ct);
        Task<bool> IsDescendantAsync(int ancestorId, int candidateId, CancellationToken ct);
        Task AddAsync(TourType entity, CancellationToken ct);
        void Remove(TourType entity);
    }

    public interface ITourRepository : IQueryableRepository<Tour>
    {
        Task<Tour> GetByIdAsync(int id, CancellationToken ct);
        Task AddAsync(Tour entity, CancellationToken ct);
        void RemoveAvailableDates(IEnumerable<TourAvailableDate> dates);
        void Remove(Tour entity);
    }

    public interface ITourServiceRepository : IQueryableRepository<TourService>
    {
        Task<TourService> GetByIdAsync(int tourId, int id, CancellationToken ct);
        Task<IReadOnlyCollection<TourService>> ListByTourIdAsync(int tourId, CancellationToken ct);
        Task<int> GetNextOrderIndexAsync(int tourId, CancellationToken ct);
        Task<bool> HasChildrenAsync(int id, CancellationToken ct);
        Task<bool> IsDescendantAsync(int ancestorId, int candidateId, CancellationToken ct);
        Task<TourService> GetWithFlightsAsync(int tourId, int id, CancellationToken ct);
        void ReplaceFlightGroups(TourService service, IEnumerable<TourFlightGroup> groups);
        Task AddAsync(TourService entity, CancellationToken ct);
        void Remove(TourService entity);
    }
}
