using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions.Persistence.Tours;
using TourCore.Domain.Tours;

namespace TourCore.Infrastructure.SqlServer.Persistence.Repositories.Tours
{
    public class TourTypeRepository : ITourTypeRepository
    {
        private readonly TourCoreDbContext c;

        public TourTypeRepository(TourCoreDbContext c)
        {
            this.c = c;
        }

        public IQueryable<TourType> Query()
        {
            return c.TourTypes;
        }

        public Task<TourType> GetByIdAsync(int id, CancellationToken ct)
        {
            return c.TourTypes.FirstOrDefaultAsync(x => x.Id == id, ct);
        }

        public async Task<IReadOnlyCollection<TourType>> GetByIdsAsync(
            IEnumerable<int> ids,
            CancellationToken ct
        )
        {
            return await c.TourTypes.Where(x => ids.Contains(x.Id)).ToListAsync(ct);
        }

        public Task<bool> ExistsByNameAsync(string name, int? excludeId, CancellationToken ct)
        {
            return c.TourTypes.AnyAsync(
                x => x.Name == name && (!excludeId.HasValue || x.Id != excludeId),
                ct
            );
        }

        public Task<bool> HasChildrenAsync(int id, CancellationToken ct)
        {
            return c.TourTypes.AnyAsync(x => x.ParentId == id, ct);
        }

        public Task<bool> IsUsedAsync(int id, CancellationToken ct)
        {
            return c.Tours.AnyAsync(x => x.TourTypes.Any(t => t.Id == id), ct);
        }

        public async Task<bool> IsDescendantAsync(
            int ancestorId,
            int candidateId,
            CancellationToken ct
        )
        {
            var current = await c
                .TourTypes.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == candidateId, ct);
            var seen = new HashSet<int>();
            while (current != null && current.ParentId.HasValue && seen.Add(current.Id))
            {
                if (current.ParentId == ancestorId)
                    return true;
                current = await c
                    .TourTypes.AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == current.ParentId.Value, ct);
            }
            return false;
        }

        public Task AddAsync(TourType e, CancellationToken ct)
        {
            c.TourTypes.Add(e);
            return Task.CompletedTask;
        }

        public void Remove(TourType e)
        {
            c.TourTypes.Remove(e);
        }
    }

    public class TourRepository : ITourRepository
    {
        private readonly TourCoreDbContext c;

        public TourRepository(TourCoreDbContext c)
        {
            this.c = c;
        }

        public IQueryable<Tour> Query()
        {
            return c.Tours;
        }

        public Task<Tour> GetByIdAsync(int id, CancellationToken ct)
        {
            return c
                .Tours.Include(x => x.AvailableDates)
                .Include(x => x.TourTypes)
                .FirstOrDefaultAsync(x => x.Id == id, ct);
        }

        public Task AddAsync(Tour e, CancellationToken ct)
        {
            c.Tours.Add(e);
            return Task.CompletedTask;
        }

        public void RemoveAvailableDates(IEnumerable<TourAvailableDate> dates)
        {
            c.TourAvailableDates.RemoveRange(dates);
        }

        public void Remove(Tour e)
        {
            c.Tours.Remove(e);
        }
    }

    public class TourServiceRepository : ITourServiceRepository
    {
        private readonly TourCoreDbContext c;

        public TourServiceRepository(TourCoreDbContext c)
        {
            this.c = c;
        }

        public IQueryable<TourService> Query()
        {
            return c.TourServices;
        }

        public Task<TourService> GetByIdAsync(int tourId, int id, CancellationToken ct)
        {
            return c.TourServices.FirstOrDefaultAsync(x => x.TourId == tourId && x.Id == id, ct);
        }

        public Task<TourService> GetWithFlightsAsync(int tourId, int id, CancellationToken ct)
        {
            return c
                .TourServices.Include(x => x.FlightGroups.Select(g => g.Flights))
                .FirstOrDefaultAsync(x => x.TourId == tourId && x.Id == id, ct);
        }

        public void ReplaceFlightGroups(TourService service, IEnumerable<TourFlightGroup> groups)
        {
            c.TourFlightGroups.RemoveRange(service.FlightGroups);
            service.FlightGroups.Clear();
            foreach (var group in groups)
                service.FlightGroups.Add(group);
        }

        public async Task<IReadOnlyCollection<TourService>> ListByTourIdAsync(
            int tourId,
            CancellationToken ct
        )
        {
            return await c
                .TourServices.Where(x => x.TourId == tourId)
                .OrderBy(x => x.OrderIndex)
                .ToListAsync(ct);
        }

        public async Task<int> GetNextOrderIndexAsync(int tourId, CancellationToken ct)
        {
            var m = await c
                .TourServices.Where(x => x.TourId == tourId)
                .Select(x => (int?)x.OrderIndex)
                .MaxAsync(ct);
            return (m ?? 0) + 1;
        }

        public Task<bool> HasChildrenAsync(int id, CancellationToken ct)
        {
            return c.TourServices.AnyAsync(x => x.ParentServiceId == id, ct);
        }

        public async Task<bool> IsDescendantAsync(
            int ancestorId,
            int candidateId,
            CancellationToken ct
        )
        {
            var current = await c
                .TourServices.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == candidateId, ct);
            var seen = new HashSet<int>();
            while (current != null && current.ParentServiceId.HasValue && seen.Add(current.Id))
            {
                if (current.ParentServiceId == ancestorId)
                    return true;
                current = await c
                    .TourServices.AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == current.ParentServiceId.Value, ct);
            }
            return false;
        }

        public Task AddAsync(TourService x, CancellationToken ct)
        {
            c.TourServices.Add(x);
            return Task.CompletedTask;
        }

        public void Remove(TourService x)
        {
            c.TourServices.Remove(x);
        }
    }
}
