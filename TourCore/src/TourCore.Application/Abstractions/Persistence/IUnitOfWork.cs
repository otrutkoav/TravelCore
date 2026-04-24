using System.Threading;
using System.Threading.Tasks;

namespace TourCore.Application.Abstractions.Persistence
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}