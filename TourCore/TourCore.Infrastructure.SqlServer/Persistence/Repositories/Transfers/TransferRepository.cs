using System.Collections.Generic;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions.Persistence.Transfers;
using TourCore.Domain.Transfers.Entities;
using System.Linq;

namespace TourCore.Infrastructure.SqlServer.Persistence.Repositories.Transfers
{
    public class TransferRepository : ITransferRepository
    {
        private readonly TourCoreDbContext _context;

        public TransferRepository(TourCoreDbContext context)
        {
            _context = context;
        }
        public IQueryable<Transfer> Query()
        {
            return _context.Transfers;
        }
        public async Task<Transfer> GetByIdAsync(int id, CancellationToken ct)
        {
            return await _context.Transfers
                .FirstOrDefaultAsync(x => x.Id == id, ct);
        }

        public async Task<IReadOnlyCollection<Transfer>> ListAsync(CancellationToken ct)
        {
            return await _context.Transfers
                .ToListAsync(ct);
        }

        public Task AddAsync(Transfer transfer, CancellationToken ct)
        {
            _context.Transfers.Add(transfer);
            return Task.CompletedTask;
        }
    }
}