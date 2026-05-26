using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence.Hotels;
using TourCore.Application.Common.Queries;
using TourCore.Application.Hotels.HotelCategories.Queries;
using TourCore.Contracts.Common;
using TourCore.Contracts.Hotels.HotelCategories;

namespace TourCore.Application.Hotels.HotelCategories.Handlers
{
    public class GetHotelCategoriesHandler : IQueryHandler<GetHotelCategoriesQuery, PagedResponseDto<HotelCategoryListItemDto>>
    {
        private readonly IHotelCategoryRepository _hotelCategoryRepository;
        private readonly PagedQueryExecutor _pagedQueryExecutor;

        public GetHotelCategoriesHandler(
            IHotelCategoryRepository hotelCategoryRepository,
            PagedQueryExecutor pagedQueryExecutor)
        {
            _hotelCategoryRepository = hotelCategoryRepository;
            _pagedQueryExecutor = pagedQueryExecutor;
        }

        public async Task<PagedResponseDto<HotelCategoryListItemDto>> Handle(
            GetHotelCategoriesQuery query,
            CancellationToken cancellationToken)
        {
            query = query ?? new GetHotelCategoriesQuery();

            var hotelCategories = _hotelCategoryRepository.Query();

            if (query.Filter != null && !string.IsNullOrWhiteSpace(query.Filter.Search))
            {
                var search = query.Filter.Search.Trim();

                hotelCategories = hotelCategories.Where(x =>
                    x.Name.Contains(search) ||
                    x.NameEn.Contains(search) ||
                    x.GlobalCode.Contains(search));
            }

            if (string.IsNullOrWhiteSpace(query.SortBy))
            {
                hotelCategories = hotelCategories
                    .OrderBy(x => x.PrintOrder)
                    .ThenBy(x => x.Name);
            }
            else
            {
                hotelCategories = hotelCategories.ApplySorting(
                    query,
                    HotelCategorySortDefinition.Instance);
            }

            var dtoQuery = hotelCategories.Select(HotelCategoryProjections.ListItem);

            var result = await _pagedQueryExecutor.ExecuteAsync(
                dtoQuery,
                query,
                cancellationToken);

            return new PagedResponseDto<HotelCategoryListItemDto>(
                result.Items,
                result.Page,
                result.PageSize,
                result.TotalCount);
        }
    }
}