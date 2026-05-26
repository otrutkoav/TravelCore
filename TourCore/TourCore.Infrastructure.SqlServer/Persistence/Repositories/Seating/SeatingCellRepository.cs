using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions.Persistence.Seating;
using TourCore.Domain.Seating.Entities;

namespace TourCore.Infrastructure.SqlServer.Persistence.Repositories.Seating
{
    public class SeatingCellRepository : ISeatingCellRepository
    {
        private readonly TourCoreDbContext _context;

        public SeatingCellRepository(TourCoreDbContext context)
        {
            _context = context;
        }

        public IQueryable<SeatingCell> Query()
        {
            return _context.SeatingCells;
        }

        public async Task<SeatingCell> GetByIdAsync(int id, CancellationToken ct)
        {
            return await _context.SeatingCells.FirstOrDefaultAsync(x => x.Id == id, ct);
        }

        public async Task<IReadOnlyCollection<SeatingCell>> ListAsync(CancellationToken ct)
        {
            return await _context.SeatingCells.ToListAsync(ct);
        }

        public Task AddAsync(SeatingCell seatingCell, CancellationToken ct)
        {
            _context.SeatingCells.Add(seatingCell);
            return Task.CompletedTask;
        }
    }
}