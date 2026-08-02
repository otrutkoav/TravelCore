using System.Threading;
using System.Threading.Tasks;

namespace TourCore.Search.Application.Abstractions
{
    public interface IQueryHandler<in TQuery, TResult>
    {
        Task<TResult> Handle(TQuery query, CancellationToken cancellationToken);
    }
}
