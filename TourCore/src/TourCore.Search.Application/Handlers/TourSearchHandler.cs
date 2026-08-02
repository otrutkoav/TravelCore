using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Search.Application.Abstractions;
using TourCore.Search.Application.Models;
using TourCore.Search.Application.Validators;
using TourCore.Search.Contracts;

namespace TourCore.Search.Application.Handlers
{
    public sealed class TourSearchHandler : IQueryHandler<TourSearchRequest, TourSearchResponse>
    {
        private readonly ITourSearchDataSource _dataSource;
        private readonly TourSearchRequestValidator _validator;

        public TourSearchHandler(ITourSearchDataSource dataSource, TourSearchRequestValidator validator)
        {
            _dataSource = dataSource;
            _validator = validator;
        }

        public async Task<TourSearchResponse> Handle(TourSearchRequest request, CancellationToken cancellationToken)
        {
            _validator.ValidateAndNormalize(request);
            var data = await _dataSource.LoadAsync(request, cancellationToken);
            var transfers = data.Transfers.GroupBy(x => x.CityId)
                .ToDictionary(x => x.Key, x => x.OrderBy(y => y.ShowOrder).ThenBy(y => y.Id).ToArray());
            var candidates = new List<TourCandidate>();

            foreach (var date in request.Dates)
            foreach (var nights in request.DurationsInNights)
            foreach (var hotel in data.Hotels)
            {
                cancellationToken.ThrowIfCancellationRequested();
                if (request.DepartureCityId == -1)
                {
                    candidates.Add(CreateCandidate(request, hotel, date, nights, null, transfers));
                    continue;
                }

                var returnDate = date.AddDays(nights);
                var outbound = data.Routes.Where(x => x.DepartureCityId == request.DepartureCityId &&
                    x.ArrivalCityId == hotel.CityId && RunsOn(x, date)).ToArray();
                var inbound = data.Routes.Where(x => x.DepartureCityId == hotel.CityId &&
                    x.ArrivalCityId == request.DepartureCityId && RunsOn(x, returnDate)).ToArray();

                foreach (var outgoing in outbound)
                foreach (var incoming in inbound.Where(x => x.Kind == outgoing.Kind))
                    candidates.Add(CreateCandidate(request, hotel, date, nights, new RoundTripTransport
                    {
                        Kind = outgoing.Kind,
                        Outbound = Map(outgoing, date),
                        Inbound = Map(incoming, returnDate)
                    }, transfers));
            }

            var ordered = candidates.OrderBy(x => x.StartDate).ThenBy(x => x.Nights)
                .ThenBy(x => x.Destination.CityName).ThenBy(x => x.Hotel.Name)
                .ThenBy(x => x.Transport == null ? 0 : (int)x.Transport.Kind)
                .ThenBy(x => x.CandidateId).ToList();
            var page = ordered.Skip((request.Page - 1) * request.PageSize).Take(request.PageSize).ToArray();
            return new TourSearchResponse(new SearchPagedResult<TourCandidate>(page, request.Page, request.PageSize, ordered.Count));
        }

        private static bool RunsOn(SearchRouteRow route, DateTime date)
        {
            if (route.DateFrom.HasValue && date.Date < route.DateFrom.Value.Date) return false;
            if (route.DateTo.HasValue && date.Date > route.DateTo.Value.Date) return false;
            if (string.IsNullOrWhiteSpace(route.DaysOfWeek)) return true;
            var day = date.DayOfWeek == DayOfWeek.Sunday ? '7' : (char)('0' + (int)date.DayOfWeek);
            return route.DaysOfWeek.IndexOf(day) >= 0;
        }

        private static RouteSegment Map(SearchRouteRow route, DateTime date)
        {
            var departure = date.Date.Add(route.TimeFrom.HasValue ? route.TimeFrom.Value.TimeOfDay : TimeSpan.Zero);
            var arrival = date.Date.AddDays(route.DaysOnRoad ?? 0)
                .Add(route.TimeTo.HasValue ? route.TimeTo.Value.TimeOfDay : TimeSpan.Zero);
            if (arrival < departure) arrival = arrival.AddDays(1);
            return new RouteSegment
            {
                RouteId = route.RouteId, ScheduleId = route.ScheduleId, Name = route.Name,
                DepartureCityId = route.DepartureCityId, DepartureCityName = route.DepartureCityName,
                ArrivalCityId = route.ArrivalCityId, ArrivalCityName = route.ArrivalCityName,
                DepartureDateTime = departure, ArrivalDateTime = arrival,
                CarrierCode = route.CarrierCode, Number = route.Number
            };
        }

        private static TourCandidate CreateCandidate(TourSearchRequest request, SearchHotelRow hotel,
            DateTime date, int nights, RoundTripTransport transport,
            IDictionary<int, SearchTransferRow[]> transfers)
        {
            SearchTransferRow[] cityTransfers;
            transfers.TryGetValue(hotel.CityId, out cityTransfers);
            var key = transport == null ? "hotel" : ((int)transport.Kind) + "-" +
                transport.Outbound.ScheduleId + "-" + transport.Inbound.ScheduleId;
            return new TourCandidate
            {
                CandidateId = string.Format("{0:yyyyMMdd}-{1}-{2}-{3}", date, nights, hotel.Id, key),
                StartDate = date, EndDate = date.AddDays(nights), Nights = nights, Adults = request.Adults,
                Destination = new SearchDestination { CountryId = hotel.CountryId, CountryName = hotel.CountryName,
                    CityId = hotel.CityId, CityName = hotel.CityName },
                Hotel = new SearchHotel { Id = hotel.Id, Code = hotel.Code, Name = hotel.Name, Stars = hotel.Stars,
                    CategoryId = hotel.CategoryId, CategoryName = hotel.CategoryName, ResortId = hotel.ResortId,
                    ResortName = hotel.ResortName, Address = hotel.Address, Rank = hotel.Rank },
                Transport = transport,
                Transfers = (cityTransfers ?? new SearchTransferRow[0]).Select(x => new SearchTransfer
                {
                    Id = x.Id, Name = x.Name, DurationText = x.DurationText,
                    PlaceFrom = x.PlaceFrom, PlaceTo = x.PlaceTo
                }).ToArray()
            };
        }
    }
}
