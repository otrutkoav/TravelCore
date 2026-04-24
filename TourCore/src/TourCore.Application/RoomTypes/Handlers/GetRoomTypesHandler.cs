using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Common.Models;
using TourCore.Application.RoomTypes.DTOs;
using TourCore.Application.RoomTypes.Mappings;
using TourCore.Application.RoomTypes.Queries;

namespace TourCore.Application.RoomTypes.Handlers
{
    public class GetRoomTypesHandler : IQueryHandler<GetRoomTypesQuery, ListResult<RoomTypeListItemDto>>
    {
        private readonly IRoomTypeRepository _roomTypeRepository;

        public GetRoomTypesHandler(IRoomTypeRepository roomTypeRepository)
        {
            _roomTypeRepository = roomTypeRepository;
        }

        public async Task<ListResult<RoomTypeListItemDto>> Handle(GetRoomTypesQuery query, CancellationToken cancellationToken)
        {
            var entities = await _roomTypeRepository.ListAsync(cancellationToken);
            var items = entities.AsEnumerable();

            if (query != null && query.Filter != null && !string.IsNullOrWhiteSpace(query.Filter.Search))
            {
                var search = query.Filter.Search.Trim();

                items = items.Where(x =>
                    (!string.IsNullOrWhiteSpace(x.Code) && x.Code.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    (!string.IsNullOrWhiteSpace(x.Name) && x.Name.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    (!string.IsNullOrWhiteSpace(x.NameEn) && x.NameEn.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0));
            }

            var result = items
                .OrderBy(x => x.SortOrder)
                .ThenBy(x => x.Name)
                .Select(x => x.ToListItemDto())
                .ToArray();

            return ListResult<RoomTypeListItemDto>.Create(result);
        }
    }
}