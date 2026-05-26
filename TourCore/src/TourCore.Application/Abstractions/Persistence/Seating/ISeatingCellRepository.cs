using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Common.Data;
using TourCore.Domain.Seating.Entities;

namespace TourCore.Application.Abstractions.Persistence.Seating
{
    public interface ISeatingCellRepository : IQueryableRepository<SeatingCell>
    {
        Task<SeatingCell> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<IReadOnlyCollection<SeatingCell>> ListAsync(CancellationToken cancellationToken);

        Task AddAsync(SeatingCell seatingCell, CancellationToken cancellationToken);
    }
}