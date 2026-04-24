using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.BusSchedules.DTOs;
using TourCore.Application.BusSchedules.Mappings;
using TourCore.Application.BusSchedules.Queries;
using TourCore.Application.Common.Models;

namespace TourCore.Application.BusSchedules.Handlers
{
    public class GetBusSchedulesHandler : IQueryHandler<GetBusSchedulesQuery, ListResult<BusScheduleListItemDto>>
    {
        private readonly IBusScheduleRepository _busScheduleRepository;

        public GetBusSchedulesHandler(IBusScheduleRepository busScheduleRepository)
        {
            _busScheduleRepository = busScheduleRepository;
        }

        public async Task<ListResult<BusScheduleListItemDto>> Handle(GetBusSchedulesQuery query, CancellationToken cancellationToken)
        {
            var entities = await _busScheduleRepository.ListAsync(cancellationToken);
            var items = entities.AsEnumerable();

            if (query != null && query.Filter != null)
            {
                if (query.Filter.BusTransferId.HasValue)
                    items = items.Where(x => x.BusTransferId == query.Filter.BusTransferId.Value);
            }

            var result = items
                .OrderBy(x => x.BusTransferId)
                .ThenBy(x => x.DateFrom)
                .ThenBy(x => x.DateTo)
                .Select(x => x.ToListItemDto())
                .ToArray();

            return ListResult<BusScheduleListItemDto>.Create(result);
        }
    }
}