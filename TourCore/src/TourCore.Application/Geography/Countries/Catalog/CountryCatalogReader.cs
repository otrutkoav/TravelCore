using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions.Caching;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Common.Data;
using TourCore.Application.Geography.Countries.Mappings;
using TourCore.Application.Geography.Countries.Queries;
using TourCore.Contracts.Geography.Countries;
using TourCore.Domain.Geography.Entities;
using TourCore.Application.Common.Data;

namespace TourCore.Application.Geography.Countries.Catalog
{
    public class CountryCatalogReader : ICountryCatalogReader
    {
        private readonly IQueryableRepository<Country> _repository;
        private readonly ICatalogCacheProvider _cacheProvider;
        private readonly IAsyncQueryExecutor _asyncQueryExecutor;

        public CountryCatalogReader(
            IQueryableRepository<Country> repository,
            ICatalogCacheProvider cacheProvider,
            IAsyncQueryExecutor asyncQueryExecutor)
        {
            _repository = repository;
            _cacheProvider = cacheProvider;
            _asyncQueryExecutor = asyncQueryExecutor;
        }

        public Task<IReadOnlyCollection<CountryListItemDto>> GetAllAsync(
            CancellationToken cancellationToken)
        {
            return _cacheProvider.GetOrCreateAsync(
                CatalogCacheKeys.Countries,
                async () =>
                {
                    var query = _repository
                        .Query()
                        .OrderBy(x => x.SortOrder)
                        .ThenBy(x => x.Name)
                        .Select(CountryProjections.ListItem);

                    var items = await _asyncQueryExecutor
                        .ToArrayAsync(query, cancellationToken);

                    return (IReadOnlyCollection<CountryListItemDto>)items;
                },
                cancellationToken);
        }
    }
}