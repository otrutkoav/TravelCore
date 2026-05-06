using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions.Persistence.Finance;
using TourCore.Domain.Finance.Entities;
using System.Linq;

namespace TourCore.Infrastructure.SqlServer.Persistence.Repositories.Finance
{
    public class RealCourseRepository : IRealCourseRepository
    {
        private readonly TourCoreDbContext _context;

        public RealCourseRepository(TourCoreDbContext context)
        {
            _context = context;
        }

        public IQueryable<RealCourse> Query()
        {
            return _context.RealCourses;
        }

        public async Task<RealCourse> GetByIdAsync(int id, CancellationToken ct)
        {
            return await _context.RealCourses.FirstOrDefaultAsync(x => x.Id == id, ct);
        }

        public async Task<IReadOnlyCollection<RealCourse>> ListAsync(CancellationToken ct)
        {
            return await _context.RealCourses.ToListAsync(ct);
        }

        public async Task<bool> ExistsAsync(
            string fromRateCode,
            string toRateCode,
            DateTime? dateBeg,
            DateTime? dateEnd,
            CancellationToken ct)
        {
            return await _context.RealCourses.AnyAsync(
                x => x.FromRateCode == fromRateCode &&
                     x.ToRateCode == toRateCode &&
                     x.DateBeg == dateBeg &&
                     x.DateEnd == dateEnd,
                ct);
        }

        public async Task<bool> ExistsAsync(
            string fromRateCode,
            string toRateCode,
            DateTime? dateBeg,
            DateTime? dateEnd,
            int excludeId,
            CancellationToken ct)
        {
            return await _context.RealCourses.AnyAsync(
                x => x.Id != excludeId &&
                     x.FromRateCode == fromRateCode &&
                     x.ToRateCode == toRateCode &&
                     x.DateBeg == dateBeg &&
                     x.DateEnd == dateEnd,
                ct);
        }

        public Task AddAsync(RealCourse realCourse, CancellationToken ct)
        {
            _context.RealCourses.Add(realCourse);
            return Task.CompletedTask;
        }
    }
}