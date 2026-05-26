using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence.Hotels;
using TourCore.Application.Common.Queries;
using TourCore.Application.Hotels.Hotels.Queries;
using TourCore.Contracts.Common;
using TourCore.Contracts.Hotels.Hotels;

namespace TourCore.Application.Hotels.Hotels.Handlers
{
    public class GetHotelsHandler : IQueryHandler<GetHotelsQuery, PagedResponseDto<HotelListItemDto>>
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly PagedQueryExecutor _pagedQueryExecutor;

        public GetHotelsHandler(
            IHotelRepository hotelRepository,
            PagedQueryExecutor pagedQueryExecutor)
        {
            _hotelRepository = hotelRepository;
            _pagedQueryExecutor = pagedQueryExecutor;
        }

        public async Task<PagedResponseDto<HotelListItemDto>> Handle(
            GetHotelsQuery query,
            CancellationToken cancellationToken)
        {
            query = query ?? new GetHotelsQuery();

            var hotels = _hotelRepository.Query();

            if (query.Filter != null)
            {
                if (!string.IsNullOrWhiteSpace(query.Filter.Search))
                {
                    var search = query.Filter.Search.Trim();

                    hotels = hotels.Where(x =>
                        x.Code.Contains(search) ||
                        x.Name.Contains(search) ||
                        x.NameEn.Contains(search));
                }

                if (query.Filter.CountryId.HasValue)
                {
                    hotels = hotels.Where(x =>
                        x.CountryId == query.Filter.CountryId.Value);
                }

                if (query.Filter.CityId.HasValue)
                {
                    hotels = hotels.Where(x =>
                        x.CityId == query.Filter.CityId.Value);
                }

                if (query.Filter.ResortId.HasValue)
                {
                    hotels = hotels.Where(x =>
                        x.ResortId == query.Filter.ResortId.Value);
                }

                if (query.Filter.CategoryId.HasValue)
                {
                    hotels = hotels.Where(x =>
                        x.CategoryId == query.Filter.CategoryId.Value);
                }

                if (query.Filter.IsCruise.HasValue)
                {
                    hotels = hotels.Where(x =>
                        x.IsCruise == query.Filter.IsCruise.Value);
                }
            }

            if (string.IsNullOrWhiteSpace(query.SortBy))
            {
                hotels = hotels
                    .OrderBy(x => x.SortOrder)
                    .ThenBy(x => x.Name);
            }
            else
            {
                hotels = hotels.ApplySorting(
                    query,
                    HotelSortDefinition.Instance);
            }

            var dtoQuery = hotels.Select(HotelProjections.ListItem);

            var result = await _pagedQueryExecutor.ExecuteAsync(
                dtoQuery,
                query,
                cancellationToken);

            return new PagedResponseDto<HotelListItemDto>(
                result.Items,
                result.Page,
                result.PageSize,
                result.TotalCount);
        }
    }
}