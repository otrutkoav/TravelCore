using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Contracts.Bus.BusSchedules;
using TourCore.Application.Common.Models;
using TourCore.Application.Abstractions.Persistence.Bus;
using TourCore.Application.Bus.BusSchedules.Mappings;
using TourCore.Application.Bus.BusSchedules.Queries;

namespace TourCore.Application.Bus.BusSchedules.Handlers
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