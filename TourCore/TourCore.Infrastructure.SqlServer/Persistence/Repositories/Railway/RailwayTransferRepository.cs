using System.Collections.Generic;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions.Persistence.Railway;
using TourCore.Domain.Railway.Entities;
using System.Linq;

namespace TourCore.Infrastructure.SqlServer.Persistence.Repositories.Railway
{
    public class RailwayTransferRepository : IRailwayTransferRepository
    {
        private readonly TourCoreDbContext _context;

        public RailwayTransferRepository(TourCoreDbContext context)
        {
            _context = context;
        }
        public IQueryable<RailwayTransfer> Query()
        {
            return _context.RailwayTransfers;
        }
        public async Task<RailwayTransfer> GetByIdAsync(int id, CancellationToken ct)
        {
            return await _context.RailwayTransfers.FirstOrDefaultAsync(x => x.Id == id, ct);
        }

        public async Task<IReadOnlyCollection<RailwayTransfer>> ListAsync(CancellationToken ct)
        {
            return await _context.RailwayTransfers.ToListAsync(ct);
        }

        public Task AddAsync(RailwayTransfer railwayTransfer, CancellationToken ct)
        {
            _context.RailwayTransfers.Add(railwayTransfer);
            return Task.CompletedTask;
        }
    }
}