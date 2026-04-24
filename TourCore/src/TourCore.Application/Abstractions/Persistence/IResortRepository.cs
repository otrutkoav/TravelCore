using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Domain.Geography.Entities;

namespace TourCore.Application.Abstractions.Persistence
{
    public interface IResortRepository
    {
        Task<Resort> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<IReadOnlyCollection<Resort>> ListAsync(CancellationToken cancellationToken);

        Task AddAsync(Resort resort, CancellationToken cancellationToken);
    }
}