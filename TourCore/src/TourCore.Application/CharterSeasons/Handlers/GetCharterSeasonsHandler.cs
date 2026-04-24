using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Contracts.Avia.CharterSeasons;
using TourCore.Application.CharterSeasons.Mappings;
using TourCore.Application.CharterSeasons.Queries;
using TourCore.Application.Common.Models;

namespace TourCore.Application.CharterSeasons.Handlers
{
    public class GetCharterSeasonsHandler : IQueryHandler<GetCharterSeasonsQuery, ListResult<CharterSeasonListItemDto>>
    {
        private readonly ICharterSeasonRepository _charterSeasonRepository;

        public GetCharterSeasonsHandler(ICharterSeasonRepository charterSeasonRepository)
        {
            _charterSeasonRepository = charterSeasonRepository;
        }

        public async Task<ListResult<CharterSeasonListItemDto>> Handle(GetCharterSeasonsQuery query, CancellationToken cancellationToken)
        {
            var entities = await _charterSeasonRepository.ListAsync(cancellationToken);
            var items = entities.AsEnumerable();

            if (query != null && query.Filter != null)
            {
                if (query.Filter.CharterId.HasValue)
                    items = items.Where(x => x.CharterId == query.Filter.CharterId.Value);
            }

            var result = items
                .OrderBy(x => x.CharterId)
                .ThenBy(x => x.DateFrom)
                .ThenBy(x => x.DateTo)
                .Select(x => x.ToListItemDto())
                .ToArray();

            return ListResult<CharterSeasonListItemDto>.Create(result);
        }
    }
}