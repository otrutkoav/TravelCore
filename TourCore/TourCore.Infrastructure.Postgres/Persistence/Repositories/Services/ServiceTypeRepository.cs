using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TourCore.Application.Abstractions.Persistence.Services;
using TourCore.Domain.Services.Entities;

namespace TourCore.Infrastructure.Postgres.Persistence.Repositories.Services
{
    public class ServiceTypeRepository : IServiceTypeRepository
    {
        private readonly TourCoreDbContext _context;
        public ServiceTypeRepository(TourCoreDbContext context) { _context = context; }

        public IQueryable<ServiceType> Query() { return _context.ServiceTypes; }
        public Task<ServiceType> GetByIdAsync(int id, CancellationToken ct) { return _context.ServiceTypes.FirstOrDefaultAsync(x => x.Id == id, ct); }
        public async Task<IReadOnlyCollection<ServiceType>> ListAsync(CancellationToken ct) { return await _context.ServiceTypes.ToListAsync(ct); }
        public Task<bool> ExistsByCodeAsync(string code, CancellationToken ct) { return _context.ServiceTypes.AnyAsync(x => x.Code == code, ct); }
        public Task<bool> ExistsByCodeAsync(string code, int excludeId, CancellationToken ct) { return _context.ServiceTypes.AnyAsync(x => x.Code == code && x.Id != excludeId, ct); }
        public Task AddAsync(ServiceType entity, CancellationToken ct) { _context.ServiceTypes.Add(entity); return Task.CompletedTask; }
        public void Remove(ServiceType entity) { _context.ServiceTypes.Remove(entity); }
    }
}
