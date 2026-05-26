using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence.Hotels;
using TourCore.Application.Common.Queries;
using TourCore.Application.Hotels.RoomTypes.Queries;
using TourCore.Contracts.Common;
using TourCore.Contracts.Hotels.RoomTypes;

namespace TourCore.Application.Hotels.RoomTypes.Handlers
{
    public class GetRoomTypesHandler : IQueryHandler<GetRoomTypesQuery, PagedResponseDto<RoomTypeListItemDto>>
    {
        private readonly IRoomTypeRepository _roomTypeRepository;
        private readonly PagedQueryExecutor _pagedQueryExecutor;

        public GetRoomTypesHandler(
            IRoomTypeRepository roomTypeRepository,
            PagedQueryExecutor pagedQueryExecutor)
        {
            _roomTypeRepository = roomTypeRepository;
            _pagedQueryExecutor = pagedQueryExecutor;
        }

        public async Task<PagedResponseDto<RoomTypeListItemDto>> Handle(
            GetRoomTypesQuery query,
            CancellationToken cancellationToken)
        {
            query = query ?? new GetRoomTypesQuery();

            var roomTypes = _roomTypeRepository.Query();

            if (query.Filter != null && !string.IsNullOrWhiteSpace(query.Filter.Search))
            {
                var search = query.Filter.Search.Trim();

                roomTypes = roomTypes.Where(x =>
                    x.Code.Contains(search) ||
                    x.Name.Contains(search) ||
                    x.NameEn.Contains(search));
            }

            if (string.IsNullOrWhiteSpace(query.SortBy))
            {
                roomTypes = roomTypes
                    .OrderBy(x => x.SortOrder)
                    .ThenBy(x => x.Name);
            }
            else
            {
                roomTypes = roomTypes.ApplySorting(
                    query,
                    RoomTypeSortDefinition.Instance);
            }

            var dtoQuery = roomTypes.Select(RoomTypeProjections.ListItem);

            var result = await _pagedQueryExecutor.ExecuteAsync(
                dtoQuery,
                query,
                cancellationToken);

            return new PagedResponseDto<RoomTypeListItemDto>(
                result.Items,
                result.Page,
                result.PageSize,
                result.TotalCount);
        }
    }
}