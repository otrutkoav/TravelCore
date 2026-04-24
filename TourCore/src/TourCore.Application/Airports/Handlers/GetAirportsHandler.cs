using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Contracts.Avia.Airports;
using TourCore.Application.Airports.Mappings;
using TourCore.Application.Airports.Queries;
using TourCore.Application.Common.Models;

namespace TourCore.Application.Airports.Handlers
{
    public class GetAirportsHandler : IQueryHandler<GetAirportsQuery, ListResult<AirportListItemDto>>
    {
        private readonly IAirportRepository _airportRepository;

        public GetAirportsHandler(IAirportRepository airportRepository)
        {
            _airportRepository = airportRepository;
        }

        public async Task<ListResult<AirportListItemDto>> Handle(GetAirportsQuery query, CancellationToken cancellationToken)
        {
            var entities = await _airportRepository.ListAsync(cancellationToken);
            var items = entities.AsEnumerable();

            if (query != null && query.Filter != null)
            {
                if (!string.IsNullOrWhiteSpace(query.Filter.Search))
                {
                    var search = query.Filter.Search.Trim();

                    items = items.Where(x =>
                        (!string.IsNullOrWhiteSpace(x.Code) && x.Code.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0) ||
                        (!string.IsNullOrWhiteSpace(x.Name) && x.Name.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0) ||
                        (!string.IsNullOrWhiteSpace(x.NameEn) && x.NameEn.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0) ||
                        (!string.IsNullOrWhiteSpace(x.IcaoCode) && x.IcaoCode.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0) ||
                        (!string.IsNullOrWhiteSpace(x.LetterCode) && x.LetterCode.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0));
                }

                if (query.Filter.CityId.HasValue)
                    items = items.Where(x => x.CityId == query.Filter.CityId.Value);

                if (!string.IsNullOrWhiteSpace(query.Filter.IcaoCode))
                {
                    var icaoCode = query.Filter.IcaoCode.Trim();

                    items = items.Where(x =>
                        !string.IsNullOrWhiteSpace(x.IcaoCode) &&
                        x.IcaoCode.IndexOf(icaoCode, StringComparison.OrdinalIgnoreCase) >= 0);
                }
            }

            var result = items
                .OrderBy(x => x.Name)
                .ThenBy(x => x.Code)
                .Select(x => x.ToListItemDto())
                .ToArray();

            return ListResult<AirportListItemDto>.Create(result);
        }
    }
}