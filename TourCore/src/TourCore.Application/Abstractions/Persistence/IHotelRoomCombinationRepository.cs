using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Domain.Hotels.Entities;

namespace TourCore.Application.Abstractions.Persistence
{
    public interface IHotelRoomCombinationRepository
    {
        Task<HotelRoomCombination> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<IReadOnlyCollection<HotelRoomCombination>> ListAsync(CancellationToken cancellationToken);

        Task<bool> ExistsAsync(
            int roomTypeId,
            int roomCategoryId,
            int accommodationTypeId,
            CancellationToken cancellationToken);

        Task<bool> ExistsAsync(
            int roomTypeId,
            int roomCategoryId,
            int accommodationTypeId,
            int excludeId,
            CancellationToken cancellationToken);

        Task AddAsync(HotelRoomCombination hotelRoomCombination, CancellationToken cancellationToken);
    }
}