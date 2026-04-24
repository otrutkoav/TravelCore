using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Domain.Hotels.Entities;

namespace TourCore.Application.Abstractions.Persistence
{
    public interface IMealTypeRepository
    {
        Task<MealType> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<IReadOnlyCollection<MealType>> ListAsync(CancellationToken cancellationToken);

        Task<bool> ExistsByCodeAsync(string code, CancellationToken cancellationToken);
        Task<bool> ExistsByCodeAsync(string code, int excludeId, CancellationToken cancellationToken);

        Task<bool> ExistsByGlobalCodeAsync(string globalCode, CancellationToken cancellationToken);
        Task<bool> ExistsByGlobalCodeAsync(string globalCode, int excludeId, CancellationToken cancellationToken);

        Task AddAsync(MealType mealType, CancellationToken cancellationToken);
    }
}