using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Common.Data;
using TourCore.Domain.Avia.Entities;

namespace TourCore.Application.Abstractions.Persistence.Avia
{
    public interface ICharterSeasonRepository : IQueryableRepository<CharterSeason>
    {
        Task<CharterSeason> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<IReadOnlyCollection<CharterSeason>> ListAsync(CancellationToken cancellationToken);

        Task AddAsync(CharterSeason charterSeason, CancellationToken cancellationToken);
    }
}