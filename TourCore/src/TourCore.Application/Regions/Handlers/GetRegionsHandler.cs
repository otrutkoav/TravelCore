using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Common.Models;
using TourCore.Contracts.Geography.Regions;
using TourCore.Application.Regions.Mappings;
using TourCore.Application.Regions.Queries;

namespace TourCore.Application.Regions.Handlers
{
    public class GetRegionsHandler : IQueryHandler<GetRegionsQuery, ListResult<RegionListItemDto>>
    {
        private readonly IRegionRepository _regionRepository;

        public GetRegionsHandler(IRegionRepository regionRepository)
        {
            _regionRepository = regionRepository;
        }

        public async Task<ListResult<RegionListItemDto>> Handle(GetRegionsQuery query, CancellationToken cancellationToken)
        {
            var entities = await _regionRepository.ListAsync(cancellationToken);
            var items = entities.AsEnumerable();

            if (query != null && query.Filter != null)
            {
                if (!string.IsNullOrWhiteSpace(query.Filter.Search))
                {
                    var search = query.Filter.Search.Trim();

                    items = items.Where(x =>
                        (!string.IsNullOrWhiteSpace(x.Name) && x.Name.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0) ||
                        (!string.IsNullOrWhiteSpace(x.NameEn) && x.NameEn.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0) ||
                        (!string.IsNullOrWhiteSpace(x.Code) && x.Code.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0));
                }

                if (query.Filter.CountryId.HasValue)
                    items = items.Where(x => x.CountryId == query.Filter.CountryId.Value);
            }

            var result = items
                .OrderBy(x => x.SortOrder)
                .ThenBy(x => x.Name)
                .Select(x => x.ToListItemDto())
                .ToArray();

            return ListResult<RegionListItemDto>.Create(result);
        }
    }
}