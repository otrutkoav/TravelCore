using System.Collections.Generic;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions.Persistence.Transfers;
using TourCore.Domain.Transfers.Entities;
using System.Linq;

namespace TourCore.Infrastructure.SqlServer.Persistence.Repositories.Transfers
{
    public class TransferDirectionRepository : ITransferDirectionRepository
    {
        private readonly TourCoreDbContext _context;

        public TransferDirectionRepository(TourCoreDbContext context)
        {
            _context = context;
        }
        public IQueryable<TransferDirection> Query()
        {
            return _context.TransferDirections;
        }
        public async Task<TransferDirection> GetByIdAsync(int id, CancellationToken ct)
        {
            return await _context.TransferDirections.FirstOrDefaultAsync(x => x.Id == id, ct);
        }

        public async Task<IReadOnlyCollection<TransferDirection>> ListAsync(CancellationToken ct)
        {
            return await _context.TransferDirections.ToListAsync(ct);
        }

        public Task AddAsync(TransferDirection transferDirection, CancellationToken ct)
        {
            _context.TransferDirections.Add(transferDirection);
            return Task.CompletedTask;
        }
    }
}