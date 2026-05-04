using System.Collections.Generic;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions.Persistence.Avia;
using TourCore.Domain.Avia.Entities;

namespace TourCore.Infrastructure.SqlServer.Persistence.Repositories.Avia
{
    public class CharterRepository : ICharterRepository
    {
        private readonly TourCoreDbContext _context;

        public CharterRepository(TourCoreDbContext context)
        {
            _context = context;
        }

        public async Task<Charter> GetByIdAsync(int id, CancellationToken ct)
        {
            return await _context.Charters.FirstOrDefaultAsync(x => x.Id == id, ct);
        }

        public async Task<IReadOnlyCollection<Charter>> ListAsync(CancellationToken ct)
        {
            return await _context.Charters.ToListAsync(ct);
        }

        public Task AddAsync(Charter charter, CancellationToken ct)
        {
            _context.Charters.Add(charter);
            return Task.CompletedTask;
        }
    }
}