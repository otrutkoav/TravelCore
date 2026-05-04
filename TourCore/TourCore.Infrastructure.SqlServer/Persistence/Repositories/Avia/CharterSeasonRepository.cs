using System.Collections.Generic;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions.Persistence.Avia;
using TourCore.Domain.Avia.Entities;

namespace TourCore.Infrastructure.SqlServer.Persistence.Repositories.Avia
{
    public class CharterSeasonRepository : ICharterSeasonRepository
    {
        private readonly TourCoreDbContext _context;

        public CharterSeasonRepository(TourCoreDbContext context)
        {
            _context = context;
        }

        public async Task<CharterSeason> GetByIdAsync(int id, CancellationToken ct)
        {
            return await _context.CharterSeasons.FirstOrDefaultAsync(x => x.Id == id, ct);
        }

        public async Task<IReadOnlyCollection<CharterSeason>> ListAsync(CancellationToken ct)
        {
            return await _context.CharterSeasons.ToListAsync(ct);
        }

        public Task AddAsync(CharterSeason charterSeason, CancellationToken ct)
        {
            _context.CharterSeasons.Add(charterSeason);
            return Task.CompletedTask;
        }
    }
}