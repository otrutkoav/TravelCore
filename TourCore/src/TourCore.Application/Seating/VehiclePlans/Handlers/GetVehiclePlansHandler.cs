using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence.Seating;
using TourCore.Application.Common.Queries;
using TourCore.Application.Seating.VehiclePlans.Queries;
using TourCore.Contracts.Common;
using TourCore.Contracts.Seating.VehiclePlans;

namespace TourCore.Application.Seating.VehiclePlans.Handlers
{
    public class GetVehiclePlansHandler : IQueryHandler<GetVehiclePlansQuery, PagedResponseDto<VehiclePlanListItemDto>>
    {
        private readonly IVehiclePlanRepository _vehiclePlanRepository;
        private readonly PagedQueryExecutor _pagedQueryExecutor;

        public GetVehiclePlansHandler(
            IVehiclePlanRepository vehiclePlanRepository,
            PagedQueryExecutor pagedQueryExecutor)
        {
            _vehiclePlanRepository = vehiclePlanRepository;
            _pagedQueryExecutor = pagedQueryExecutor;
        }

        public async Task<PagedResponseDto<VehiclePlanListItemDto>> Handle(
            GetVehiclePlansQuery query,
            CancellationToken cancellationToken)
        {
            query = query ?? new GetVehiclePlansQuery();

            var vehiclePlans = _vehiclePlanRepository.Query();

            if (query.Filter != null)
            {
                if (query.Filter.TransportId.HasValue)
                {
                    vehiclePlans = vehiclePlans.Where(x =>
                        x.TransportId == query.Filter.TransportId.Value);
                }

                if (query.Filter.IsAircraft.HasValue)
                {
                    vehiclePlans = vehiclePlans.Where(x =>
                        x.IsAircraft == query.Filter.IsAircraft.Value);
                }

                if (!string.IsNullOrWhiteSpace(query.Filter.Search))
                {
                    var search = query.Filter.Search.Trim();

                    vehiclePlans = vehiclePlans.Where(x =>
                        x.Name.Contains(search));
                }
            }

            if (string.IsNullOrWhiteSpace(query.SortBy))
            {
                vehiclePlans = vehiclePlans
                    .OrderBy(x => x.TransportId)
                    .ThenBy(x => x.AreaNumber)
                    .ThenBy(x => x.Name);
            }
            else
            {
                vehiclePlans = vehiclePlans.ApplySorting(
                    query,
                    VehiclePlanSortDefinition.Instance);
            }

            var dtoQuery = vehiclePlans.Select(VehiclePlanProjections.ListItem);

            var result = await _pagedQueryExecutor.ExecuteAsync(
                dtoQuery,
                query,
                cancellationToken);

            return new PagedResponseDto<VehiclePlanListItemDto>(
                result.Items,
                result.Page,
                result.PageSize,
                result.TotalCount);
        }
    }
}