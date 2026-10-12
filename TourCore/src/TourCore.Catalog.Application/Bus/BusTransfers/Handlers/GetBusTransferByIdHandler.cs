using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence.Bus;
using TourCore.Application.Bus.BusTransfers.Mappings;
using TourCore.Application.Bus.BusTransfers.Queries;
using TourCore.Application.Common.Errors;
using TourCore.Application.Common.Exceptions;
using TourCore.Contracts.Bus.BusTransfers;

namespace TourCore.Application.Bus.BusTransfers.Handlers
{
    public class GetBusTransferByIdHandler : IQueryHandler<GetBusTransferByIdQuery, BusTransferDto>
    {
        private readonly IBusTransferRepository _busTransferRepository;

        public GetBusTransferByIdHandler(IBusTransferRepository busTransferRepository)
        {
            _busTransferRepository = busTransferRepository;
        }

        public async Task<BusTransferDto> Handle(GetBusTransferByIdQuery query, CancellationToken cancellationToken)
        {
            var entity = await _busTransferRepository.GetByIdAsync(query.Id, cancellationToken);

            if (entity == null)
                throw new NotFoundException(ErrorMessages.BusTransferNotFound, ErrorCode.BusTransferNotFound);

            return entity.ToDto();
        }
    }
}