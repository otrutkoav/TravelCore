using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Contracts.Avia.Airlines;
using TourCore.Application.Airlines.Mappings;
using TourCore.Application.Airlines.Queries;
using TourCore.Application.Common.Models;

namespace TourCore.Application.Airlines.Handlers
{
    public class GetAirlinesHandler : IQueryHandler<GetAirlinesQuery, ListResult<AirlineListItemDto>>
    {
        private readonly IAirlineRepository _airlineRepository;

        public GetAirlinesHandler(IAirlineRepository airlineRepository)
        {
            _airlineRepository = airlineRepository;
        }

        public async Task<ListResult<AirlineListItemDto>> Handle(GetAirlinesQuery query, CancellationToken cancellationToken)
        {
            var entities = await _airlineRepository.ListAsync(cancellationToken);
            var items = entities.AsEnumerable();

            if (query != null && query.Filter != null)
            {
                if (!string.IsNullOrWhiteSpace(query.Filter.Search))
                {
                    var search = query.Filter.Search.Trim();

                    items = items.Where(x =>
                        (!string.IsNullOrWhiteSpace(x.Code) && x.Code.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0) ||
                        (!string.IsNullOrWhiteSpace(x.Name) && x.Name.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0) ||
                        (!string.IsNullOrWhiteSpace(x.NameEn) && x.NameEn.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0) ||
                        (!string.IsNullOrWhiteSpace(x.IcaoCode) && x.IcaoCode.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0));
                }

                if (!string.IsNullOrWhiteSpace(query.Filter.IcaoCode))
                {
                    var icaoCode = query.Filter.IcaoCode.Trim();

                    items = items.Where(x =>
                        !string.IsNullOrWhiteSpace(x.IcaoCode) &&
                        x.IcaoCode.IndexOf(icaoCode, StringComparison.OrdinalIgnoreCase) >= 0);
                }
            }

            var result = items
                .OrderBy(x => x.Name)
                .ThenBy(x => x.Code)
                .Select(x => x.ToListItemDto())
                .ToArray();

            return ListResult<AirlineListItemDto>.Create(result);
        }
    }
}