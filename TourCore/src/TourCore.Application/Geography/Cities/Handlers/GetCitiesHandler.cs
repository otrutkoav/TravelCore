using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence.Geography;
using TourCore.Application.Common.Queries;
using TourCore.Application.Geography.Cities.Queries;
using TourCore.Contracts.Common;
using TourCore.Contracts.Geography.Cities;

namespace TourCore.Application.Geography.Cities.Handlers
{
    public class GetCitiesHandler : IQueryHandler<GetCitiesQuery, PagedResponseDto<CityListItemDto>>
    {
        private readonly ICityRepository _cityRepository;
        private readonly PagedQueryExecutor _pagedQueryExecutor;

        public GetCitiesHandler(
            ICityRepository cityRepository,
            PagedQueryExecutor pagedQueryExecutor)
        {
            _cityRepository = cityRepository;
            _pagedQueryExecutor = pagedQueryExecutor;
        }

        public async Task<PagedResponseDto<CityListItemDto>> Handle(
            GetCitiesQuery query,
            CancellationToken cancellationToken)
        {
            query = query ?? new GetCitiesQuery();

            var cities = _cityRepository.Query();

            if (query.Filter != null)
            {
                if (!string.IsNullOrWhiteSpace(query.Filter.Search))
                {
                    var search = query.Filter.Search.Trim();

                    cities = cities.Where(x =>
                        x.Name.Contains(search) ||
                        x.NameEn.Contains(search) ||
                        x.Code.Contains(search) ||
                        x.IataCode.Contains(search));
                }

                if (query.Filter.CountryId.HasValue)
                {
                    var countryId = query.Filter.CountryId.Value;

                    cities = cities.Where(x => x.CountryId == countryId);
                }

                if (query.Filter.RegionId.HasValue)
                {
                    var regionId = query.Filter.RegionId.Value;

                    cities = cities.Where(x => x.RegionId == regionId);
                }

                if (query.Filter.IsDeparturePoint.HasValue)
                {
                    var isDeparturePoint = query.Filter.IsDeparturePoint.Value;

                    cities = cities.Where(x => x.IsDeparturePoint == isDeparturePoint);
                }
            }

            if (string.IsNullOrWhiteSpace(query.SortBy))
            {
                cities = cities
                    .OrderBy(x => x.SortOrder)
                    .ThenBy(x => x.Name);
            }
            else
            {
                cities = cities.ApplySorting(
                    query,
                    CitySortDefinition.Instance);
            }

            var dtoQuery = cities.Select(CityProjections.ListItem);

            var result = await _pagedQueryExecutor.ExecuteAsync(
                dtoQuery,
                query,
                cancellationToken);

            return new PagedResponseDto<CityListItemDto>(
                result.Items,
                result.Page,
                result.PageSize,
                result.TotalCount);
        }
    }
}