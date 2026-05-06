using System.Collections.Generic;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions.Persistence.Transportation;
using TourCore.Domain.Transportation.Entities;
using System.Linq;

namespace TourCore.Infrastructure.SqlServer.Persistence.Repositories.Transportation
{
    public class TransportRepository : ITransportRepository
    {
        private readonly TourCoreDbContext _context;

        public TransportRepository(TourCoreDbContext context)
        {
            _context = context;
        }
        public IQueryable<Transport> Query()
        {
            return _context.Transports;
        }
        public async Task<Transport> GetByIdAsync(int id, CancellationToken ct)
        {
            return await _context.Transports.FirstOrDefaultAsync(x => x.Id == id, ct);
        }

        public async Task<IReadOnlyCollection<Transport>> ListAsync(CancellationToken ct)
        {
            return await _context.Transports.ToListAsync(ct);
        }

        public Task AddAsync(Transport transport, CancellationToken ct)
        {
            _context.Transports.Add(transport);
            return Task.CompletedTask;
        }
    }
}