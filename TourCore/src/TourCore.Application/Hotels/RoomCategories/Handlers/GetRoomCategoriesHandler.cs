using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence.Hotels;
using TourCore.Application.Common.Queries;
using TourCore.Application.Hotels.RoomCategories.Queries;
using TourCore.Contracts.Common;
using TourCore.Contracts.Hotels.RoomCategories;

namespace TourCore.Application.Hotels.RoomCategories.Handlers
{
    public class GetRoomCategoriesHandler : IQueryHandler<GetRoomCategoriesQuery, PagedResponseDto<RoomCategoryListItemDto>>
    {
        private readonly IRoomCategoryRepository _roomCategoryRepository;
        private readonly PagedQueryExecutor _pagedQueryExecutor;

        public GetRoomCategoriesHandler(
            IRoomCategoryRepository roomCategoryRepository,
            PagedQueryExecutor pagedQueryExecutor)
        {
            _roomCategoryRepository = roomCategoryRepository;
            _pagedQueryExecutor = pagedQueryExecutor;
        }

        public async Task<PagedResponseDto<RoomCategoryListItemDto>> Handle(
            GetRoomCategoriesQuery query,
            CancellationToken cancellationToken)
        {
            query = query ?? new GetRoomCategoriesQuery();

            var roomCategories = _roomCategoryRepository.Query();

            if (query.Filter != null && !string.IsNullOrWhiteSpace(query.Filter.Search))
            {
                var search = query.Filter.Search.Trim();

                roomCategories = roomCategories.Where(x =>
                    x.Code.Contains(search) ||
                    x.Name.Contains(search) ||
                    x.NameEn.Contains(search));
            }

            if (string.IsNullOrWhiteSpace(query.SortBy))
            {
                roomCategories = roomCategories
                    .OrderBy(x => x.SortOrder)
                    .ThenBy(x => x.Name);
            }
            else
            {
                roomCategories = roomCategories.ApplySorting(
                    query,
                    RoomCategorySortDefinition.Instance);
            }

            var dtoQuery = roomCategories.Select(RoomCategoryProjections.ListItem);

            var result = await _pagedQueryExecutor.ExecuteAsync(
                dtoQuery,
                query,
                cancellationToken);

            return new PagedResponseDto<RoomCategoryListItemDto>(
                result.Items,
                result.Page,
                result.PageSize,
                result.TotalCount);
        }
    }
}