using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence.Services;
using TourCore.Application.Common.Queries;
using TourCore.Application.Services.ServiceTypes.Queries;
using TourCore.Contracts.Common;
using TourCore.Contracts.Services.ServiceTypes;

namespace TourCore.Application.Services.ServiceTypes.Handlers
{
    public class GetServiceTypesHandler : IQueryHandler<GetServiceTypesQuery, PagedResponseDto<ServiceTypeListItemDto>>
    {
        private readonly IServiceTypeRepository _serviceTypeRepository;
        private readonly PagedQueryExecutor _pagedQueryExecutor;

        public GetServiceTypesHandler(IServiceTypeRepository serviceTypeRepository, PagedQueryExecutor pagedQueryExecutor)
        {
            _serviceTypeRepository = serviceTypeRepository;
            _pagedQueryExecutor = pagedQueryExecutor;
        }

        public async Task<PagedResponseDto<ServiceTypeListItemDto>> Handle(GetServiceTypesQuery query, CancellationToken cancellationToken)
        {
            query = query ?? new GetServiceTypesQuery();
            var serviceTypes = _serviceTypeRepository.Query();

            if (query.Filter != null)
            {
                if (!string.IsNullOrWhiteSpace(query.Filter.Search))
                {
                    var search = query.Filter.Search.Trim();

                    serviceTypes = serviceTypes.Where(x =>
                        x.Name.Contains(search) || x.NameEn.Contains(search) || x.Code.Contains(search));
                }

                if (query.Filter.IsRoute.HasValue)
                    serviceTypes = serviceTypes.Where(x => x.IsRoute == query.Filter.IsRoute.Value);

                if (query.Filter.IsDuration.HasValue)
                    serviceTypes = serviceTypes.Where(x => x.IsDuration == query.Filter.IsDuration.Value);
            }

            serviceTypes = string.IsNullOrWhiteSpace(query.SortBy)
                ? serviceTypes.OrderBy(x => x.Name).ThenBy(x => x.Code)
                : serviceTypes.ApplySorting(query, ServiceTypeSortDefinition.Instance);

            var result = await _pagedQueryExecutor.ExecuteAsync(
                serviceTypes.Select(ServiceTypeProjections.ListItem), query, cancellationToken);

            return new PagedResponseDto<ServiceTypeListItemDto>(
                result.Items, result.Page, result.PageSize, result.TotalCount);
        }
    }
}
