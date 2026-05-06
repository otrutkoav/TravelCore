using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence.Hotels;
using TourCore.Application.Common.Models;
using TourCore.Application.Hotels.AccommodationPlacementRules.Mappings;
using TourCore.Application.Hotels.AccommodationPlacementRules.Queries;
using TourCore.Contracts.Hotels.AccommodationPlacementRules;

namespace TourCore.Application.Hotels.AccommodationPlacementRules.Handlers
{
    public class GetAccommodationPlacementRulesHandler : IQueryHandler<GetAccommodationPlacementRulesQuery, ListResult<AccommodationPlacementRuleListItemDto>>
    {
        private readonly IAccommodationPlacementRuleRepository _repository;

        public GetAccommodationPlacementRulesHandler(IAccommodationPlacementRuleRepository repository)
        {
            _repository = repository;
        }

        public async Task<ListResult<AccommodationPlacementRuleListItemDto>> Handle(GetAccommodationPlacementRulesQuery query, CancellationToken cancellationToken)
        {
            var entities = await _repository.ListAsync(cancellationToken);
            var items = entities.AsEnumerable();

            if (query != null && query.Filter != null)
            {
                if (query.Filter.AdultsCount.HasValue)
                    items = items.Where(x => x.AdultsCount == query.Filter.AdultsCount.Value);

                if (query.Filter.ChildrenCount.HasValue)
                    items = items.Where(x => x.ChildrenCount == query.Filter.ChildrenCount.Value);

                if (query.Filter.ChildrenAreInfants.HasValue)
                    items = items.Where(x => x.ChildrenAreInfants == query.Filter.ChildrenAreInfants.Value);
            }

            var result = items
                .OrderBy(x => x.AdultsCount)
                .ThenBy(x => x.ChildrenCount)
                .ThenBy(x => x.ChildrenAreInfants)
                .Select(x => x.ToListItemDto())
                .ToArray();

            return ListResult<AccommodationPlacementRuleListItemDto>.Create(result);
        }
    }
}