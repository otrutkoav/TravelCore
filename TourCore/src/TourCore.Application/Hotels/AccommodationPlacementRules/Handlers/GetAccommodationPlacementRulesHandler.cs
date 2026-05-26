using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence.Hotels;
using TourCore.Application.Common.Queries;
using TourCore.Application.Hotels.AccommodationPlacementRules.Queries;
using TourCore.Contracts.Common;
using TourCore.Contracts.Hotels.AccommodationPlacementRules;

namespace TourCore.Application.Hotels.AccommodationPlacementRules.Handlers
{
    public class GetAccommodationPlacementRulesHandler
        : IQueryHandler<GetAccommodationPlacementRulesQuery, PagedResponseDto<AccommodationPlacementRuleListItemDto>>
    {
        private readonly IAccommodationPlacementRuleRepository _repository;
        private readonly PagedQueryExecutor _pagedQueryExecutor;

        public GetAccommodationPlacementRulesHandler(
            IAccommodationPlacementRuleRepository repository,
            PagedQueryExecutor pagedQueryExecutor)
        {
            _repository = repository;
            _pagedQueryExecutor = pagedQueryExecutor;
        }

        public async Task<PagedResponseDto<AccommodationPlacementRuleListItemDto>> Handle(
            GetAccommodationPlacementRulesQuery query,
            CancellationToken cancellationToken)
        {
            query = query ?? new GetAccommodationPlacementRulesQuery();

            var rules = _repository.Query();

            if (query.Filter != null)
            {
                if (query.Filter.AdultsCount.HasValue)
                {
                    rules = rules.Where(x =>
                        x.AdultsCount == query.Filter.AdultsCount.Value);
                }

                if (query.Filter.ChildrenCount.HasValue)
                {
                    rules = rules.Where(x =>
                        x.ChildrenCount == query.Filter.ChildrenCount.Value);
                }

                if (query.Filter.ChildrenAreInfants.HasValue)
                {
                    rules = rules.Where(x =>
                        x.ChildrenAreInfants == query.Filter.ChildrenAreInfants.Value);
                }
            }

            if (string.IsNullOrWhiteSpace(query.SortBy))
            {
                rules = rules
                    .OrderBy(x => x.AdultsCount)
                    .ThenBy(x => x.ChildrenCount)
                    .ThenBy(x => x.ChildrenAreInfants);
            }
            else
            {
                rules = rules.ApplySorting(
                    query,
                    AccommodationPlacementRuleSortDefinition.Instance);
            }

            var dtoQuery = rules.Select(AccommodationPlacementRuleProjections.ListItem);

            var result = await _pagedQueryExecutor.ExecuteAsync(
                dtoQuery,
                query,
                cancellationToken);

            return new PagedResponseDto<AccommodationPlacementRuleListItemDto>(
                result.Items,
                result.Page,
                result.PageSize,
                result.TotalCount);
        }
    }
}