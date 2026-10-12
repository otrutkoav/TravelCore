using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions.Caching;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Common.Data;
using TourCore.Application.Common.Queries;
using TourCore.Application.Geography.Countries.Mappings;
using TourCore.Application.Geography.Countries.Queries;
using TourCore.Contracts.Geography.Countries;
using TourCore.Domain.Geography.Entities;

namespace TourCore.Application.Geography.Countries.Catalog
{
    public class CountryCatalogReader : ICountryCatalogReader
    {
        private readonly IRepository<Country> _repository;
        private readonly ICatalogCacheProvider _cacheProvider;
        private readonly IQuerySpecificationExecutor _asyncQueryExecutor;

        public CountryCatalogReader(
            IRepository<Country> repository,
            ICatalogCacheProvider cacheProvider,
            IQuerySpecificationExecutor asyncQueryExecutor)
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
                    var query = QuerySpecification.For(_repository)
                        .OrderBy(x => x.SortOrder)
                        .ThenBy(x => x.Name)
                        .Select(CountryProjections.ListItem);

                    var items = await _asyncQueryExecutor
                        .ToArrayAsync(query, null, null, cancellationToken);

                    return (IReadOnlyCollection<CountryListItemDto>)items;
                },
                cancellationToken);
        }
    }
}
