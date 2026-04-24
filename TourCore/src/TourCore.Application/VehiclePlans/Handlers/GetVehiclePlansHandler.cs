using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Common.Models;
using TourCore.Application.VehiclePlans.DTOs;
using TourCore.Application.VehiclePlans.Mappings;
using TourCore.Application.VehiclePlans.Queries;

namespace TourCore.Application.VehiclePlans.Handlers
{
    public class GetVehiclePlansHandler : IQueryHandler<GetVehiclePlansQuery, ListResult<VehiclePlanListItemDto>>
    {
        private readonly IVehiclePlanRepository _vehiclePlanRepository;

        public GetVehiclePlansHandler(IVehiclePlanRepository vehiclePlanRepository)
        {
            _vehiclePlanRepository = vehiclePlanRepository;
        }

        public async Task<ListResult<VehiclePlanListItemDto>> Handle(GetVehiclePlansQuery query, CancellationToken cancellationToken)
        {
            var entities = await _vehiclePlanRepository.ListAsync(cancellationToken);
            var items = entities.AsEnumerable();

            if (query != null && query.Filter != null)
            {
                if (query.Filter.TransportId.HasValue)
                    items = items.Where(x => x.TransportId == query.Filter.TransportId.Value);

                if (query.Filter.IsAircraft.HasValue)
                    items = items.Where(x => x.IsAircraft == query.Filter.IsAircraft.Value);

                if (!string.IsNullOrWhiteSpace(query.Filter.Search))
                {
                    var search = query.Filter.Search.Trim();

                    items = items.Where(x =>
                        !string.IsNullOrWhiteSpace(x.Name) &&
                        x.Name.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0);
                }
            }

            var result = items
                .OrderBy(x => x.TransportId)
                .ThenBy(x => x.AreaNumber)
                .ThenBy(x => x.Name)
                .Select(x => x.ToListItemDto())
                .ToArray();

            return ListResult<VehiclePlanListItemDto>.Create(result);
        }
    }
}