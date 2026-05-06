using System.Collections.Generic;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions.Persistence.Railway;
using TourCore.Domain.Railway.Entities;
using System.Linq;

namespace TourCore.Infrastructure.SqlServer.Persistence.Repositories.Railway
{
    public class TrainScheduleRepository : ITrainScheduleRepository
    {
        private readonly TourCoreDbContext _context;

        public TrainScheduleRepository(TourCoreDbContext context)
        {
            _context = context;
        }
        public IQueryable<TrainSchedule> Query()
        {
            return _context.TrainSchedules;
        }
        public async Task<TrainSchedule> GetByIdAsync(int id, CancellationToken ct)
        {
            return await _context.TrainSchedules.FirstOrDefaultAsync(x => x.Id == id, ct);
        }

        public async Task<IReadOnlyCollection<TrainSchedule>> ListAsync(CancellationToken ct)
        {
            return await _context.TrainSchedules.ToListAsync(ct);
        }

        public Task AddAsync(TrainSchedule trainSchedule, CancellationToken ct)
        {
            _context.TrainSchedules.Add(trainSchedule);
            return Task.CompletedTask;
        }
    }
}