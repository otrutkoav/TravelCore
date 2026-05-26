using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence.Avia;
using TourCore.Application.Avia.Charters.Queries;
using TourCore.Application.Common.Queries;
using TourCore.Contracts.Avia.Charters;
using TourCore.Contracts.Common;

namespace TourCore.Application.Avia.Charters.Handlers
{
    public class GetChartersHandler : IQueryHandler<GetChartersQuery, PagedResponseDto<CharterListItemDto>>
    {
        private readonly ICharterRepository _charterRepository;
        private readonly PagedQueryExecutor _pagedQueryExecutor;

        public GetChartersHandler(
            ICharterRepository charterRepository,
            PagedQueryExecutor pagedQueryExecutor)
        {
            _charterRepository = charterRepository;
            _pagedQueryExecutor = pagedQueryExecutor;
        }

        public async Task<PagedResponseDto<CharterListItemDto>> Handle(
            GetChartersQuery query,
            CancellationToken cancellationToken)
        {
            query = query ?? new GetChartersQuery();

            var charters = _charterRepository.Query();

            if (query.Filter != null)
            {
                if (query.Filter.DepartureCityId.HasValue)
                {
                    charters = charters.Where(x =>
                        x.DepartureCityId == query.Filter.DepartureCityId.Value);
                }

                if (query.Filter.ArrivalCityId.HasValue)
                {
                    charters = charters.Where(x =>
                        x.ArrivalCityId == query.Filter.ArrivalCityId.Value);
                }

                if (!string.IsNullOrWhiteSpace(query.Filter.DepartureAirportCode))
                {
                    var departureAirportCode = query.Filter.DepartureAirportCode.Trim();

                    charters = charters.Where(x =>
                        x.DepartureAirportCode.Contains(departureAirportCode));
                }

                if (!string.IsNullOrWhiteSpace(query.Filter.ArrivalAirportCode))
                {
                    var arrivalAirportCode = query.Filter.ArrivalAirportCode.Trim();

                    charters = charters.Where(x =>
                        x.ArrivalAirportCode.Contains(arrivalAirportCode));
                }

                if (!string.IsNullOrWhiteSpace(query.Filter.AirlineCode))
                {
                    var airlineCode = query.Filter.AirlineCode.Trim();

                    charters = charters.Where(x =>
                        x.AirlineCode.Contains(airlineCode));
                }

                if (!string.IsNullOrWhiteSpace(query.Filter.FlightNumber))
                {
                    var flightNumber = query.Filter.FlightNumber.Trim();

                    charters = charters.Where(x =>
                        x.FlightNumber.Contains(flightNumber));
                }
            }

            if (string.IsNullOrWhiteSpace(query.SortBy))
            {
                charters = charters
                    .OrderBy(x => x.DepartureCityId)
                    .ThenBy(x => x.ArrivalCityId)
                    .ThenBy(x => x.FlightNumber);
            }
            else
            {
                charters = charters.ApplySorting(
                    query,
                    CharterSortDefinition.Instance);
            }

            var dtoQuery = charters.Select(CharterProjections.ListItem);

            var result = await _pagedQueryExecutor.ExecuteAsync(
                dtoQuery,
                query,
                cancellationToken);

            return new PagedResponseDto<CharterListItemDto>(
                result.Items,
                result.Page,
                result.PageSize,
                result.TotalCount);
        }
    }
}