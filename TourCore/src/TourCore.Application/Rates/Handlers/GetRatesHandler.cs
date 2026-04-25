using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Common.Models;
using TourCore.Contracts.Finance.Rates;
using TourCore.Application.Rates.Mappings;
using TourCore.Application.Rates.Queries;

namespace TourCore.Application.Rates.Handlers
{
    public class GetRatesHandler : IQueryHandler<GetRatesQuery, ListResult<RateListItemDto>>
    {
        private readonly IRateRepository _rateRepository;

        public GetRatesHandler(IRateRepository rateRepository)
        {
            _rateRepository = rateRepository;
        }

        public async Task<ListResult<RateListItemDto>> Handle(GetRatesQuery query, CancellationToken cancellationToken)
        {
            var entities = await _rateRepository.ListAsync(cancellationToken);
            var items = entities.AsEnumerable();

            if (query != null && query.Filter != null)
            {
                if (!string.IsNullOrWhiteSpace(query.Filter.Search))
                {
                    var search = query.Filter.Search.Trim();

                    items = items.Where(x =>
                        (!string.IsNullOrWhiteSpace(x.Code) && x.Code.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0) ||
                        (!string.IsNullOrWhiteSpace(x.Name) && x.Name.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0) ||
                        (!string.IsNullOrWhiteSpace(x.IsoCode) && x.IsoCode.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0));
                }

                if (query.Filter.IsMain.HasValue)
                    items = items.Where(x => x.IsMain == query.Filter.IsMain.Value);

                if (query.Filter.IsNational.HasValue)
                    items = items.Where(x => x.IsNational == query.Filter.IsNational.Value);

                if (query.Filter.ShowInSearch.HasValue)
                    items = items.Where(x => x.ShowInSearch == query.Filter.ShowInSearch.Value);
            }

            var result = items
                .OrderBy(x => x.Name)
                .ThenBy(x => x.Code)
                .Select(x => x.ToListItemDto())
                .ToArray();

            return ListResult<RateListItemDto>.Create(result);
        }
    }
}