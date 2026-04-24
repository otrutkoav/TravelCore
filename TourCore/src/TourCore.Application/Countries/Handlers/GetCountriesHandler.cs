using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Common.Models;
using TourCore.Contracts.Geography.Countries;
using TourCore.Application.Countries.Mappings;
using TourCore.Application.Countries.Queries;

namespace TourCore.Application.Countries.Handlers
{
    public class GetCountriesHandler : IQueryHandler<GetCountriesQuery, ListResult<CountryListItemDto>>
    {
        private readonly ICountryRepository _countryRepository;

        public GetCountriesHandler(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public async Task<ListResult<CountryListItemDto>> Handle(GetCountriesQuery query, CancellationToken cancellationToken)
        {
            var entities = await _countryRepository.ListAsync(cancellationToken);

            var items = entities.AsEnumerable();

            if (query != null && query.Filter != null && !string.IsNullOrWhiteSpace(query.Filter.Search))
            {
                var search = query.Filter.Search.Trim();

                items = items.Where(x =>
                    (!string.IsNullOrWhiteSpace(x.Name) && x.Name.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    (!string.IsNullOrWhiteSpace(x.NameEn) && x.NameEn.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    (!string.IsNullOrWhiteSpace(x.Code) && x.Code.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    (!string.IsNullOrWhiteSpace(x.IsoCode2) && x.IsoCode2.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    (!string.IsNullOrWhiteSpace(x.IsoCode3) && x.IsoCode3.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0));
            }

            var result = items
                .OrderBy(x => x.SortOrder)
                .ThenBy(x => x.Name)
                .Select(x => x.ToListItemDto())
                .ToArray();

            return ListResult<CountryListItemDto>.Create(result);
        }
    }
}