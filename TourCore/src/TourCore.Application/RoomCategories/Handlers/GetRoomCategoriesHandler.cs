using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Common.Models;
using TourCore.Application.RoomCategories.DTOs;
using TourCore.Application.RoomCategories.Mappings;
using TourCore.Application.RoomCategories.Queries;

namespace TourCore.Application.RoomCategories.Handlers
{
    public class GetRoomCategoriesHandler : IQueryHandler<GetRoomCategoriesQuery, ListResult<RoomCategoryListItemDto>>
    {
        private readonly IRoomCategoryRepository _roomCategoryRepository;

        public GetRoomCategoriesHandler(IRoomCategoryRepository roomCategoryRepository)
        {
            _roomCategoryRepository = roomCategoryRepository;
        }

        public async Task<ListResult<RoomCategoryListItemDto>> Handle(GetRoomCategoriesQuery query, CancellationToken cancellationToken)
        {
            var entities = await _roomCategoryRepository.ListAsync(cancellationToken);
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

            return ListResult<RoomCategoryListItemDto>.Create(result);
        }
    }
}