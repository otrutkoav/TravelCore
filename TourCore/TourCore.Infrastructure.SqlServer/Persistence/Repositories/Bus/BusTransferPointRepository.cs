using System.Collections.Generic;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions.Persistence.Bus;
using TourCore.Domain.Bus.Entities;
using System.Linq;

namespace TourCore.Infrastructure.SqlServer.Persistence.Repositories.Bus
{
    public class BusTransferPointRepository : IBusTransferPointRepository
    {
        private readonly TourCoreDbContext _context;

        public BusTransferPointRepository(TourCoreDbContext context)
        {
            _context = context;
        }

        public IQueryable<BusTransferPoint> Query()
        {
            return _context.BusTransferPoints;
        }

        public async Task<BusTransferPoint> GetByIdAsync(int id, CancellationToken ct)
        {
            return await _context.BusTransferPoints.FirstOrDefaultAsync(x => x.Id == id, ct);
        }

        public async Task<IReadOnlyCollection<BusTransferPoint>> ListAsync(CancellationToken ct)
        {
            return await _context.BusTransferPoints.ToListAsync(ct);
        }

        public Task AddAsync(BusTransferPoint busTransferPoint, CancellationToken ct)
        {
            _context.BusTransferPoints.Add(busTransferPoint);
            return Task.CompletedTask;
        }
    }
}