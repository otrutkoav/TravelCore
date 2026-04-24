using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Domain.Seating.Entities;

namespace TourCore.Application.Abstractions.Persistence
{
    public interface ISeatingCellRepository
    {
        Task<SeatingCell> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<IReadOnlyCollection<SeatingCell>> ListAsync(CancellationToken cancellationToken);

        Task AddAsync(SeatingCell seatingCell, CancellationToken cancellationToken);
    }
}