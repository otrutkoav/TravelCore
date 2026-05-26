using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Common.Data;
using TourCore.Domain.Hotels.Entities;

namespace TourCore.Application.Abstractions.Persistence.Hotels
{
    public interface IMealTypeRepository : IQueryableRepository<MealType>
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