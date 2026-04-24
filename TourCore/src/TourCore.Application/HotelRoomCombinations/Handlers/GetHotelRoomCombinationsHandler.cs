using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Common.Models;
using TourCore.Application.HotelRoomCombinations.DTOs;
using TourCore.Application.HotelRoomCombinations.Mappings;
using TourCore.Application.HotelRoomCombinations.Queries;

namespace TourCore.Application.HotelRoomCombinations.Handlers
{
    public class GetHotelRoomCombinationsHandler : IQueryHandler<GetHotelRoomCombinationsQuery, ListResult<HotelRoomCombinationListItemDto>>
    {
        private readonly IHotelRoomCombinationRepository _hotelRoomCombinationRepository;

        public GetHotelRoomCombinationsHandler(IHotelRoomCombinationRepository hotelRoomCombinationRepository)
        {
            _hotelRoomCombinationRepository = hotelRoomCombinationRepository;
        }

        public async Task<ListResult<HotelRoomCombinationListItemDto>> Handle(GetHotelRoomCombinationsQuery query, CancellationToken cancellationToken)
        {
            var entities = await _hotelRoomCombinationRepository.ListAsync(cancellationToken);
            var items = entities.AsEnumerable();

            if (query != null && query.Filter != null)
            {
                if (query.Filter.RoomTypeId.HasValue)
                    items = items.Where(x => x.RoomTypeId == query.Filter.RoomTypeId.Value);

                if (query.Filter.RoomCategoryId.HasValue)
                    items = items.Where(x => x.RoomCategoryId == query.Filter.RoomCategoryId.Value);

                if (query.Filter.AccommodationTypeId.HasValue)
                    items = items.Where(x => x.AccommodationTypeId == query.Filter.AccommodationTypeId.Value);

                if (query.Filter.IsMain.HasValue)
                    items = items.Where(x => x.IsMain == query.Filter.IsMain.Value);
            }

            var result = items
                .OrderBy(x => x.RoomTypeId)
                .ThenBy(x => x.RoomCategoryId)
                .ThenBy(x => x.AccommodationTypeId)
                .Select(x => x.ToListItemDto())
                .ToArray();

            return ListResult<HotelRoomCombinationListItemDto>.Create(result);
        }
    }
}