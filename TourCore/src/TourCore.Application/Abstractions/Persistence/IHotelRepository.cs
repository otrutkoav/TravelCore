using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Domain.Hotels.Entities;

namespace TourCore.Application.Abstractions.Persistence
{
    public interface IHotelRepository
    {
        Task<Hotel> GetByIdAsync(int id, CancellationToken cancellationToken);

        Task<IReadOnlyCollection<Hotel>> ListAsync(CancellationToken cancellationToken);

        Task<bool> ExistsByCodeAsync(string code, CancellationToken cancellationToken);

        Task<bool> ExistsByCodeAsync(string code, int excludeId, CancellationToken cancellationToken);

        Task AddAsync(Hotel hotel, CancellationToken cancellationToken);

        void Update(Hotel hotel);
    }
}