using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Domain.Finance.Entities;

namespace TourCore.Application.Abstractions.Persistence
{
    public interface IRateRepository
    {
        Task<Rate> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<IReadOnlyCollection<Rate>> ListAsync(CancellationToken cancellationToken);

        Task<bool> ExistsByCodeAsync(string code, CancellationToken cancellationToken);
        Task<bool> ExistsByCodeAsync(string code, int excludeId, CancellationToken cancellationToken);

        Task<bool> ExistsByIsoCodeAsync(string isoCode, CancellationToken cancellationToken);
        Task<bool> ExistsByIsoCodeAsync(string isoCode, int excludeId, CancellationToken cancellationToken);

        Task<bool> ExistsByCodeValueAsync(string code, CancellationToken cancellationToken);

        Task AddAsync(Rate rate, CancellationToken cancellationToken);
    }
}