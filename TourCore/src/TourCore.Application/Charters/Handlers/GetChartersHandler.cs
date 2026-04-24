using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Contracts.Avia.Charters;
using TourCore.Application.Charters.Mappings;
using TourCore.Application.Charters.Queries;
using TourCore.Application.Common.Models;

namespace TourCore.Application.Charters.Handlers
{
    public class GetChartersHandler : IQueryHandler<GetChartersQuery, ListResult<CharterListItemDto>>
    {
        private readonly ICharterRepository _charterRepository;

        public GetChartersHandler(ICharterRepository charterRepository)
        {
            _charterRepository = charterRepository;
        }

        public async Task<ListResult<CharterListItemDto>> Handle(GetChartersQuery query, CancellationToken cancellationToken)
        {
            var entities = await _charterRepository.ListAsync(cancellationToken);
            var items = entities.AsEnumerable();

            if (query != null && query.Filter != null)
            {
                if (query.Filter.DepartureCityId.HasValue)
                    items = items.Where(x => x.DepartureCityId == query.Filter.DepartureCityId.Value);

                if (query.Filter.ArrivalCityId.HasValue)
                    items = items.Where(x => x.ArrivalCityId == query.Filter.ArrivalCityId.Value);

                if (!string.IsNullOrWhiteSpace(query.Filter.FlightNumber))
                {
                    var flightNumber = query.Filter.FlightNumber.Trim();

                    items = items.Where(x =>
                        !string.IsNullOrWhiteSpace(x.FlightNumber) &&
                        x.FlightNumber.IndexOf(flightNumber, StringComparison.OrdinalIgnoreCase) >= 0);
                }

                if (!string.IsNullOrWhiteSpace(query.Filter.AirlineCode))
                {
                    var airlineCode = query.Filter.AirlineCode.Trim();

                    items = items.Where(x =>
                        !string.IsNullOrWhiteSpace(x.AirlineCode) &&
                        x.AirlineCode.IndexOf(airlineCode, StringComparison.OrdinalIgnoreCase) >= 0);
                }
            }

            var result = items
                .OrderBy(x => x.DepartureCityId)
                .ThenBy(x => x.ArrivalCityId)
                .ThenBy(x => x.FlightNumber)
                .Select(x => x.ToListItemDto())
                .ToArray();

            return ListResult<CharterListItemDto>.Create(result);
        }
    }
}