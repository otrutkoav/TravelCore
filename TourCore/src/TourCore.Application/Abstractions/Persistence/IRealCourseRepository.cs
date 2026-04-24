using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Domain.Finance.Entities;

namespace TourCore.Application.Abstractions.Persistence
{
    public interface IRealCourseRepository
    {
        Task<RealCourse> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<IReadOnlyCollection<RealCourse>> ListAsync(CancellationToken cancellationToken);

        Task<bool> ExistsAsync(
            string fromRateCode,
            string toRateCode,
            DateTime? dateBeg,
            DateTime? dateEnd,
            CancellationToken cancellationToken);

        Task<bool> ExistsAsync(
            string fromRateCode,
            string toRateCode,
            DateTime? dateBeg,
            DateTime? dateEnd,
            int excludeId,
            CancellationToken cancellationToken);

        Task AddAsync(RealCourse realCourse, CancellationToken cancellationToken);
    }
}