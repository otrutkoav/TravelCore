using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Contracts.Hotels.AccommodationTypes;
using TourCore.Application.AccommodationTypes.Mappings;
using TourCore.Application.AccommodationTypes.Queries;
using TourCore.Application.Common.Models;

namespace TourCore.Application.AccommodationTypes.Handlers
{
    public class GetAccommodationTypesHandler : IQueryHandler<GetAccommodationTypesQuery, ListResult<AccommodationTypeListItemDto>>
    {
        private readonly IAccommodationTypeRepository _accommodationTypeRepository;

        public GetAccommodationTypesHandler(IAccommodationTypeRepository accommodationTypeRepository)
        {
            _accommodationTypeRepository = accommodationTypeRepository;
        }

        public async Task<ListResult<AccommodationTypeListItemDto>> Handle(GetAccommodationTypesQuery query, CancellationToken cancellationToken)
        {
            var entities = await _accommodationTypeRepository.ListAsync(cancellationToken);
            var items = entities.AsEnumerable();

            if (query != null && query.Filter != null)
            {
                if (!string.IsNullOrWhiteSpace(query.Filter.Search))
                {
                    var search = query.Filter.Search.Trim();

                    items = items.Where(x =>
                        (!string.IsNullOrWhiteSpace(x.Code) && x.Code.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0) ||
                        (!string.IsNullOrWhiteSpace(x.Name) && x.Name.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0) ||
                        (!string.IsNullOrWhiteSpace(x.NameEn) && x.NameEn.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0));
                }

                if (query.Filter.IsMain.HasValue)
                    items = items.Where(x => x.IsMain == query.Filter.IsMain.Value);
            }

            var result = items
                .OrderBy(x => x.SortOrder)
                .ThenBy(x => x.Name)
                .Select(x => x.ToListItemDto())
                .ToArray();

            return ListResult<AccommodationTypeListItemDto>.Create(result);
        }
    }
}