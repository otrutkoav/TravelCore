using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Common.Models;
using TourCore.Contracts.Geography.Resorts;
using TourCore.Application.Resorts.Mappings;
using TourCore.Application.Resorts.Queries;

namespace TourCore.Application.Resorts.Handlers
{
    public class GetResortsHandler : IQueryHandler<GetResortsQuery, ListResult<ResortListItemDto>>
    {
        private readonly IResortRepository _resortRepository;

        public GetResortsHandler(IResortRepository resortRepository)
        {
            _resortRepository = resortRepository;
        }

        public async Task<ListResult<ResortListItemDto>> Handle(GetResortsQuery query, CancellationToken cancellationToken)
        {
            var entities = await _resortRepository.ListAsync(cancellationToken);
            var items = entities.AsEnumerable();

            if (query != null && query.Filter != null)
            {
                if (!string.IsNullOrWhiteSpace(query.Filter.Search))
                {
                    var search = query.Filter.Search.Trim();

                    items = items.Where(x =>
                        (!string.IsNullOrWhiteSpace(x.Name) && x.Name.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0) ||
                        (!string.IsNullOrWhiteSpace(x.NameEn) && x.NameEn.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0));
                }

                if (query.Filter.CountryId.HasValue)
                    items = items.Where(x => x.CountryId == query.Filter.CountryId.Value);
            }

            var result = items
                .OrderBy(x => x.Name)
                .Select(x => x.ToListItemDto())
                .ToArray();

            return ListResult<ResortListItemDto>.Create(result);
        }
    }
}