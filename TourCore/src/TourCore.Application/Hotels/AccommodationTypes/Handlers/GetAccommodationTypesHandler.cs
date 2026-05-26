using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence.Hotels;
using TourCore.Application.Common.Queries;
using TourCore.Application.Hotels.AccommodationTypes.Queries;
using TourCore.Contracts.Common;
using TourCore.Contracts.Hotels.AccommodationTypes;

namespace TourCore.Application.Hotels.AccommodationTypes.Handlers
{
    public class GetAccommodationTypesHandler
        : IQueryHandler<GetAccommodationTypesQuery, PagedResponseDto<AccommodationTypeListItemDto>>
    {
        private readonly IAccommodationTypeRepository _accommodationTypeRepository;
        private readonly PagedQueryExecutor _pagedQueryExecutor;

        public GetAccommodationTypesHandler(
            IAccommodationTypeRepository accommodationTypeRepository,
            PagedQueryExecutor pagedQueryExecutor)
        {
            _accommodationTypeRepository = accommodationTypeRepository;
            _pagedQueryExecutor = pagedQueryExecutor;
        }

        public async Task<PagedResponseDto<AccommodationTypeListItemDto>> Handle(
            GetAccommodationTypesQuery query,
            CancellationToken cancellationToken)
        {
            query = query ?? new GetAccommodationTypesQuery();

            var accommodationTypes = _accommodationTypeRepository.Query();

            if (query.Filter != null)
            {
                if (!string.IsNullOrWhiteSpace(query.Filter.Search))
                {
                    var search = query.Filter.Search.Trim();

                    accommodationTypes = accommodationTypes.Where(x =>
                        x.Code.Contains(search) ||
                        x.Name.Contains(search) ||
                        x.NameEn.Contains(search));
                }

                if (query.Filter.IsMain.HasValue)
                {
                    accommodationTypes = accommodationTypes.Where(x =>
                        x.IsMain == query.Filter.IsMain.Value);
                }
            }

            if (string.IsNullOrWhiteSpace(query.SortBy))
            {
                accommodationTypes = accommodationTypes
                    .OrderBy(x => x.SortOrder)
                    .ThenBy(x => x.Name);
            }
            else
            {
                accommodationTypes = accommodationTypes.ApplySorting(
                    query,
                    AccommodationTypeSortDefinition.Instance);
            }

            var dtoQuery = accommodationTypes.Select(AccommodationTypeProjections.ListItem);

            var result = await _pagedQueryExecutor.ExecuteAsync(
                dtoQuery,
                query,
                cancellationToken);

            return new PagedResponseDto<AccommodationTypeListItemDto>(
                result.Items,
                result.Page,
                result.PageSize,
                result.TotalCount);
        }
    }
}