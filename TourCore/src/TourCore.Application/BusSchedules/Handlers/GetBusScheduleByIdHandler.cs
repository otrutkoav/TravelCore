using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Contracts.Bus.BusSchedules;
using TourCore.Application.BusSchedules.Mappings;
using TourCore.Application.BusSchedules.Queries;
using TourCore.Application.Common.Exceptions;

namespace TourCore.Application.BusSchedules.Handlers
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