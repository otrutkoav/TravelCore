using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Contracts.Geography.Cities;
using TourCore.Application.Cities.Mappings;
using TourCore.Application.Cities.Queries;
using TourCore.Application.Common.Models;

namespace TourCore.Application.Cities.Handlers
{
    public class GetCitiesHandler : IQueryHandler<GetCitiesQuery, ListResult<CityListItemDto>>
    {
        private readonly ICityRepository _cityRepository;

        public GetCitiesHandler(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        public async Task<ListResult<CityListItemDto>> Handle(GetCitiesQuery query, CancellationToken cancellationToken)
        {
            var entities = await _cityRepository.ListAsync(cancellationToken);
            var items = entities.AsEnumerable();

            if (query != null && query.Filter != null)
            {
                if (!string.IsNullOrWhiteSpace(query.Filter.Search))
                {
                    var search = query.Filter.Search.Trim();

                    items = items.Where(x =>
                        (!string.IsNullOrWhiteSpace(x.Name) && x.Name.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0) ||
                        (!string.IsNullOrWhiteSpace(x.NameEn) && x.NameEn.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0) ||
                        (!string.IsNullOrWhiteSpace(x.Code) && x.Code.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0) ||
                        (!string.IsNullOrWhiteSpace(x.IataCode) && x.IataCode.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0));
                }

                if (query.Filter.CountryId.HasValue)
                    items = items.Where(x => x.CountryId == query.Filter.CountryId.Value);

                if (query.Filter.RegionId.HasValue)
                    items = items.Where(x => x.RegionId == query.Filter.RegionId.Value);

                if (query.Filter.IsDeparturePoint.HasValue)
                    items = items.Where(x => x.IsDeparturePoint == query.Filter.IsDeparturePoint.Value);
            }

            var result = items
                .OrderBy(x => x.SortOrder)
                .ThenBy(x => x.Name)
                .Select(x => x.ToListItemDto())
                .ToArray();

            return ListResult<CityListItemDto>.Create(result);
        }
    }
}