using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Common.Models;
using TourCore.Contracts.Hotels.MealTypes;
using TourCore.Application.MealTypes.Mappings;
using TourCore.Application.MealTypes.Queries;

namespace TourCore.Application.MealTypes.Handlers
{
    public class GetMealTypesHandler : IQueryHandler<GetMealTypesQuery, ListResult<MealTypeListItemDto>>
    {
        private readonly IMealTypeRepository _mealTypeRepository;

        public GetMealTypesHandler(IMealTypeRepository mealTypeRepository)
        {
            _mealTypeRepository = mealTypeRepository;
        }

        public async Task<ListResult<MealTypeListItemDto>> Handle(GetMealTypesQuery query, CancellationToken cancellationToken)
        {
            var entities = await _mealTypeRepository.ListAsync(cancellationToken);
            var items = entities.AsEnumerable();

            if (query != null && query.Filter != null)
            {
                if (!string.IsNullOrWhiteSpace(query.Filter.Search))
                {
                    var search = query.Filter.Search.Trim();

                    items = items.Where(x =>
                        (!string.IsNullOrWhiteSpace(x.Name) && x.Name.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0) ||
                        (!string.IsNullOrWhiteSpace(x.NameEn) && x.NameEn.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0) ||
                        (!string.IsNullOrWhiteSpace(x.Code) && x.Code.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0) ||
                        (!string.IsNullOrWhiteSpace(x.GlobalCode) && x.GlobalCode.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0));
                }

                if (!string.IsNullOrWhiteSpace(query.Filter.GlobalCode))
                {
                    var globalCode = query.Filter.GlobalCode.Trim();

                    items = items.Where(x =>
                        !string.IsNullOrWhiteSpace(x.GlobalCode) &&
                        x.GlobalCode.IndexOf(globalCode, StringComparison.OrdinalIgnoreCase) >= 0);
                }
            }

            var result = items
                .OrderBy(x => x.SortOrder)
                .ThenBy(x => x.Name)
                .Select(x => x.ToListItemDto())
                .ToArray();

            return ListResult<MealTypeListItemDto>.Create(result);
        }
    }
}