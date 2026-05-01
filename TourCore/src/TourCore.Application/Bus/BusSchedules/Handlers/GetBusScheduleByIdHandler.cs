using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Contracts.Bus.BusSchedules;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Abstractions.Persistence.Bus;
using TourCore.Application.Bus.BusSchedules.Mappings;
using TourCore.Application.Bus.BusSchedules.Queries;

namespace TourCore.Application.Bus.BusSchedules.Handlers
{
    public class GetBusScheduleByIdHandler : IQueryHandler<GetBusScheduleByIdQuery, BusScheduleDto>
    {
        private readonly IBusScheduleRepository _busScheduleRepository;

        public GetBusScheduleByIdHandler(IBusScheduleRepository busScheduleRepository)
        {
            _busScheduleRepository = busScheduleRepository;
        }

        public async Task<BusScheduleDto> Handle(GetBusScheduleByIdQuery query, CancellationToken cancellationToken)
        {
            var entity = await _busScheduleRepository.GetByIdAsync(query.Id, cancellationToken);
            if (entity == null)
                throw new NotFoundException("Bus schedule was not found.");

            return entity.ToDto();
        }
    }
}