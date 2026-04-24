using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Domain.Hotels.Entities;

namespace TourCore.Application.Abstractions.Persistence
{
    public interface IRoomCategoryRepository
    {
        Task<RoomCategory> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<IReadOnlyCollection<RoomCategory>> ListAsync(CancellationToken cancellationToken);

        Task<bool> ExistsByCodeAsync(string code, CancellationToken cancellationToken);
        Task<bool> ExistsByCodeAsync(string code, int excludeId, CancellationToken cancellationToken);

        Task AddAsync(RoomCategory roomCategory, CancellationToken cancellationToken);
    }
}