using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Domain.Hotels.Entities;

namespace TourCore.Application.Abstractions.Persistence
{
    public interface IRoomTypeRepository
    {
        Task<RoomType> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<IReadOnlyCollection<RoomType>> ListAsync(CancellationToken cancellationToken);

        Task<bool> ExistsByCodeAsync(string code, CancellationToken cancellationToken);
        Task<bool> ExistsByCodeAsync(string code, int excludeId, CancellationToken cancellationToken);

        Task AddAsync(RoomType roomType, CancellationToken cancellationToken);
    }
}