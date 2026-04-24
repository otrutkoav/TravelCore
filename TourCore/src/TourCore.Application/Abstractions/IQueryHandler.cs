using System.Threading;
using System.Threading.Tasks;

namespace TourCore.Application.Abstractions
{
    public interface IQueryHandler<TQuery, TResult>
    {
        Task<TResult> Handle(TQuery query, CancellationToken cancellationToken);
    }
}