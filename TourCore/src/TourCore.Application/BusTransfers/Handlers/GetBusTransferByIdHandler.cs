using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Contracts.Bus.BusTransfers;
using TourCore.Application.BusTransfers.Mappings;
using TourCore.Application.BusTransfers.Queries;
using TourCore.Application.Common.Exceptions;

namespace TourCore.Application.BusTransfers.Handlers
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