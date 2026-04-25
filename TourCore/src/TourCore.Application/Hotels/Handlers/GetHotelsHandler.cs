using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Common.Models;
using TourCore.Application.Hotels.Mappings;
using TourCore.Application.Hotels.Queries;
using TourCore.Contracts.Hotels.Hotels;

namespace TourCore.Application.Hotels.Handlers
{
    public class GetHotelsHandler : IQueryHandler<GetHotelsQuery, ListResult<HotelListItemDto>>
    {
        private readonly IHotelRepository _hotelRepository;

        public GetHotelsHandler(IHotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }

        public async Task<ListResult<HotelListItemDto>> Handle(GetHotelsQuery query, CancellationToken cancellationToken)
        {
            var entities = await _hotelRepository.ListAsync(cancellationToken);
            var items = entities.AsEnumerable();

            if (query != null && query.Filter != null)
            {
                if (query.Filter.CountryId.HasValue)
                    items = items.Where(x => x.CountryId == query.Filter.CountryId.Value);

                if (query.Filter.CityId.HasValue)
                    items = items.Where(x => x.CityId == query.Filter.CityId.Value);

                if (query.Filter.ResortId.HasValue)
                    items = items.Where(x => x.ResortId == query.Filter.ResortId.Value);

                if (query.Filter.CategoryId.HasValue)
                    items = items.Where(x => x.CategoryId == query.Filter.CategoryId.Value);

                if (query.Filter.IsCruise.HasValue)
                    items = items.Where(x => x.IsCruise == query.Filter.IsCruise.Value);

                if (!string.IsNullOrWhiteSpace(query.Filter.Search))
                {
                    var search = query.Filter.Search.Trim();

                    items = items.Where(x =>
                        (!string.IsNullOrWhiteSpace(x.Name) && x.Name.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0) ||
                        (!string.IsNullOrWhiteSpace(x.NameEn) && x.NameEn.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0) ||
                        (!string.IsNullOrWhiteSpace(x.Code) && x.Code.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0));
                }
            }

            var result = items
                .OrderBy(x => x.SortOrder)
                .ThenBy(x => x.Name)
                .Select(x => x.ToListItemDto())
                .ToArray();

            return ListResult<HotelListItemDto>.Create(result);
        }
    }
}