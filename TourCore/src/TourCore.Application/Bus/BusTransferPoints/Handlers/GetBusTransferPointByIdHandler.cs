using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Contracts.Bus.BusTransferPoints;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Abstractions.Persistence.Bus;
using TourCore.Application.Bus.BusTransferPoints.Mappings;
using TourCore.Application.Bus.BusTransferPoints.Queries;

namespace TourCore.Application.Bus.BusTransferPoints.Handlers
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
                throw new NotFoundException(ErrorMessages.BusTransferNotFound, ErrorCode.BusTransferNotFound);

            return entity.ToDto();
        }
    }
}