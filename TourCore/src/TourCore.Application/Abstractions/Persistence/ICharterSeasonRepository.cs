using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Domain.Avia.Entities;

namespace TourCore.Application.Abstractions.Persistence
{
    public interface ICharterSeasonRepository
    {
        Task<CharterSeason> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<IReadOnlyCollection<CharterSeason>> ListAsync(CancellationToken cancellationToken);

        Task AddAsync(CharterSeason charterSeason, CancellationToken cancellationToken);
    }
}