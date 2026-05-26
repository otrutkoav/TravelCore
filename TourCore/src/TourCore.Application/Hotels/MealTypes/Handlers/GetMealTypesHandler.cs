using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence.Hotels;
using TourCore.Application.Common.Queries;
using TourCore.Application.Hotels.MealTypes.Queries;
using TourCore.Contracts.Common;
using TourCore.Contracts.Hotels.MealTypes;

namespace TourCore.Application.Hotels.MealTypes.Handlers
{
    public class GetMealTypesHandler : IQueryHandler<GetMealTypesQuery, PagedResponseDto<MealTypeListItemDto>>
    {
        private readonly IMealTypeRepository _mealTypeRepository;
        private readonly PagedQueryExecutor _pagedQueryExecutor;

        public GetMealTypesHandler(
            IMealTypeRepository mealTypeRepository,
            PagedQueryExecutor pagedQueryExecutor)
        {
            _mealTypeRepository = mealTypeRepository;
            _pagedQueryExecutor = pagedQueryExecutor;
        }

        public async Task<PagedResponseDto<MealTypeListItemDto>> Handle(
            GetMealTypesQuery query,
            CancellationToken cancellationToken)
        {
            query = query ?? new GetMealTypesQuery();

            var mealTypes = _mealTypeRepository.Query();

            if (query.Filter != null)
            {
                if (!string.IsNullOrWhiteSpace(query.Filter.Search))
                {
                    var search = query.Filter.Search.Trim();

                    mealTypes = mealTypes.Where(x =>
                        x.Name.Contains(search) ||
                        x.NameEn.Contains(search) ||
                        x.Code.Contains(search) ||
                        x.GlobalCode.Contains(search));
                }

                if (!string.IsNullOrWhiteSpace(query.Filter.GlobalCode))
                {
                    var globalCode = query.Filter.GlobalCode.Trim();

                    mealTypes = mealTypes.Where(x =>
                        x.GlobalCode.Contains(globalCode));
                }
            }

            if (string.IsNullOrWhiteSpace(query.SortBy))
            {
                mealTypes = mealTypes
                    .OrderBy(x => x.SortOrder)
                    .ThenBy(x => x.Name);
            }
            else
            {
                mealTypes = mealTypes.ApplySorting(
                    query,
                    MealTypeSortDefinition.Instance);
            }

            var dtoQuery = mealTypes.Select(MealTypeProjections.ListItem);

            var result = await _pagedQueryExecutor.ExecuteAsync(
                dtoQuery,
                query,
                cancellationToken);

            return new PagedResponseDto<MealTypeListItemDto>(
                result.Items,
                result.Page,
                result.PageSize,
                result.TotalCount);
        }
    }
}