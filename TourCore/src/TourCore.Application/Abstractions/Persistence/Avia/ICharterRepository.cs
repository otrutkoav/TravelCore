using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Common.Data;
using TourCore.Domain.Avia.Entities;

namespace TourCore.Application.Abstractions.Persistence.Avia
{
    public interface ICharterRepository : IQueryableRepository<Charter>
    {
        Task<Charter> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<IReadOnlyCollection<Charter>> ListAsync(CancellationToken cancellationToken);

        Task AddAsync(Charter charter, CancellationToken cancellationToken);
    }
}