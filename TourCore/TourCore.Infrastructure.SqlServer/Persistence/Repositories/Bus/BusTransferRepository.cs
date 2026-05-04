using System.Collections.Generic;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions.Persistence.Bus;
using TourCore.Domain.Bus.Entities;

namespace TourCore.Infrastructure.SqlServer.Persistence.Repositories.Bus
{
    public class BusTransferRepository : IBusTransferRepository
    {
        private readonly TourCoreDbContext _context;

        public BusTransferRepository(TourCoreDbContext context)
        {
            _context = context;
        }

        public async Task<BusTransfer> GetByIdAsync(int id, CancellationToken ct)
        {
            return await _context.BusTransfers.FirstOrDefaultAsync(x => x.Id == id, ct);
        }

        public async Task<IReadOnlyCollection<BusTransfer>> ListAsync(CancellationToken ct)
        {
            return await _context.BusTransfers.ToListAsync(ct);
        }

        public Task AddAsync(BusTransfer busTransfer, CancellationToken ct)
        {
            _context.BusTransfers.Add(busTransfer);
            return Task.CompletedTask;
        }
    }
}