using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.BusTransferPoints.DTOs;
using TourCore.Application.BusTransferPoints.Mappings;
using TourCore.Application.BusTransferPoints.Queries;
using TourCore.Application.Common.Exceptions;

namespace TourCore.Application.BusTransferPoints.Handlers
{
    public class GetBusTransferPointByIdHandler : IQueryHandler<GetBusTransferPointByIdQuery, BusTransferPointDto>
    {
        private readonly IBusTransferPointRepository _busTransferPointRepository;

        public GetBusTransferPointByIdHandler(IBusTransferPointRepository busTransferPointRepository)
        {
            _busTransferPointRepository = busTransferPointRepository;
        }

        public async Task<BusTransferPointDto> Handle(GetBusTransferPointByIdQuery query, CancellationToken cancellationToken)
        {
            var entity = await _busTransferPointRepository.GetByIdAsync(query.Id, cancellationToken);
            if (entity == null)
                throw new NotFoundException("Bus transfer point was not found.");

            return entity.ToDto();
        }
    }
}