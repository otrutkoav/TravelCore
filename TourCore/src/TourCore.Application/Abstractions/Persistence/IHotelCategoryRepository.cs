using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Domain.Hotels.Entities;

namespace TourCore.Application.Abstractions.Persistence
{
    public interface IHotelCategoryRepository
    {
        Task<HotelCategory> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<IReadOnlyCollection<HotelCategory>> ListAsync(CancellationToken cancellationToken);

        Task<bool> ExistsByGlobalCodeAsync(string globalCode, CancellationToken cancellationToken);
        Task<bool> ExistsByGlobalCodeAsync(string globalCode, int excludeId, CancellationToken cancellationToken);

        Task AddAsync(HotelCategory hotelCategory, CancellationToken cancellationToken);
    }
}