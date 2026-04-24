using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Domain.Services.Entities;

namespace TourCore.Application.Abstractions.Persistence
{
    public interface IServiceTypeRepository
    {
        Task<ServiceType> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<IReadOnlyCollection<ServiceType>> ListAsync(CancellationToken cancellationToken);

        Task<bool> ExistsByCodeAsync(string code, CancellationToken cancellationToken);
        Task<bool> ExistsByCodeAsync(string code, int excludeId, CancellationToken cancellationToken);

        Task AddAsync(ServiceType entity, CancellationToken cancellationToken);
    }
}