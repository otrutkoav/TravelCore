using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Common.Exceptions;
using TourCore.Contracts.Transfers.TransferDirections;
using TourCore.Application.TransferDirections.Mappings;
using TourCore.Application.TransferDirections.Queries;

namespace TourCore.Application.TransferDirections.Handlers
{
    public class GetTransferDirectionByIdHandler : IQueryHandler<GetTransferDirectionByIdQuery, TransferDirectionDto>
    {
        private readonly ITransferDirectionRepository _transferDirectionRepository;

        public GetTransferDirectionByIdHandler(ITransferDirectionRepository transferDirectionRepository)
        {
            _transferDirectionRepository = transferDirectionRepository;
        }

        public async Task<TransferDirectionDto> Handle(GetTransferDirectionByIdQuery query, CancellationToken cancellationToken)
        {
            var entity = await _transferDirectionRepository.GetByIdAsync(query.Id, cancellationToken);
            if (entity == null)
                throw new NotFoundException("Transfer direction was not found.");

            return entity.ToDto();
        }
    }
}