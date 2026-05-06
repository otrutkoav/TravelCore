using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence.Geography;
using TourCore.Application.Common.Queries;
using TourCore.Application.Geography.Countries.Queries;
using TourCore.Contracts.Common;
using TourCore.Contracts.Geography.Countries;

namespace TourCore.Application.Geography.Countries.Handlers
{
    public class GetCountriesHandler : IQueryHandler<GetCountriesQuery, PagedResponseDto<CountryListItemDto>>
    {
        private readonly ICountryRepository _countryRepository;
        private readonly PagedQueryExecutor _pagedQueryExecutor;

        public GetCountriesHandler(
            ICountryRepository countryRepository,
            PagedQueryExecutor pagedQueryExecutor)
        {
            _countryRepository = countryRepository;
            _pagedQueryExecutor = pagedQueryExecutor;
        }

        public async Task<PagedResponseDto<CountryListItemDto>> Handle(
            GetCountriesQuery query,
            CancellationToken cancellationToken)
        {
            query = query ?? new GetCountriesQuery();

            var countries = _countryRepository.Query();

            if (query.Filter != null && !string.IsNullOrWhiteSpace(query.Filter.Search))
            {
                var search = query.Filter.Search.Trim();

                countries = countries.Where(x =>
                    x.Name.Contains(search) ||
                    x.NameEn.Contains(search) ||
                    x.Code.Contains(search) ||
                    x.IsoCode2.Contains(search) ||
                    x.IsoCode3.Contains(search) ||
                    x.DigitalCode.Contains(search) ||
                    x.CitizenshipName.Contains(search) ||
                    x.CitizenshipNameEn.Contains(search));
            }

            if (string.IsNullOrWhiteSpace(query.SortBy))
            {
                countries = countries
                    .OrderBy(x => x.SortOrder)
                    .ThenBy(x => x.Name);
            }
            else
            {
                countries = countries.ApplySorting(
                    query,
                    CountrySortDefinition.Instance);
            }

            var dtoQuery = countries.Select(CountryProjections.ListItem);

            var result = await _pagedQueryExecutor.ExecuteAsync(
                dtoQuery,
                query,
                cancellationToken);

            return new PagedResponseDto<CountryListItemDto>(
                result.Items,
                result.Page,
                result.PageSize,
                result.TotalCount);
        }
    }
}