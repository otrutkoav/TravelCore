using System.Collections.Generic;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions.Persistence.Bus;
using TourCore.Domain.Bus.Entities;

namespace TourCore.Infrastructure.SqlServer.Persistence.Repositories.Bus
{
    public class BusScheduleRepository : IBusScheduleRepository
    {
        private readonly TourCoreDbContext _context;

        public BusScheduleRepository(TourCoreDbContext context)
        {
            _context = context;
        }

        public async Task<BusSchedule> GetByIdAsync(int id, CancellationToken ct)
        {
            return await _context.BusSchedules.FirstOrDefaultAsync(x => x.Id == id, ct);
        }

        public async Task<IReadOnlyCollection<BusSchedule>> ListAsync(CancellationToken ct)
        {
            return await _context.BusSchedules.ToListAsync(ct);
        }

        public Task AddAsync(BusSchedule busSchedule, CancellationToken ct)
        {
            _context.BusSchedules.Add(busSchedule);
            return Task.CompletedTask;
        }
    }
}