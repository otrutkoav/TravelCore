using System.Threading;
using System.Threading.Tasks;
using TourCore.Search.Application.Models;
using TourCore.Search.Contracts;

namespace TourCore.Search.Application.Abstractions
{
    public interface ITourSearchDataSource
    {
        Task<SearchDataSet> LoadAsync(TourSearchRequest request, CancellationToken cancellationToken);
    }
}
