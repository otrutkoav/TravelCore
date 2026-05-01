using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Contracts.Bus.BusTransfers;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Abstractions.Persistence.Bus;
using TourCore.Application.Bus.BusTransfers.Mappings;
using TourCore.Application.Bus.BusTransfers.Queries;

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
                throw new NotFoundException("Bus transfer was not found.");

            return entity.ToDto();
        }
    }
}