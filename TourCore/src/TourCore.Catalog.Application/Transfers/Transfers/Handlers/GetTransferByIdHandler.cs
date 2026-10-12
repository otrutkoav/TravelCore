using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence.Transfers;
using TourCore.Application.Common.Errors;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Transfers.Transfers.Mappings;
using TourCore.Application.Transfers.Transfers.Queries;
using TourCore.Contracts.Transfers.Transfers;

namespace TourCore.Application.Transfers.Transfers.Handlers
{
    public class GetTransferByIdHandler : IQueryHandler<GetTransferByIdQuery, TransferDto>
    {
        private readonly ITransferRepository _transferRepository;

        public GetTransferByIdHandler(ITransferRepository transferRepository)
        {
            _transferRepository = transferRepository;
        }

        public async Task<TransferDto> Handle(GetTransferByIdQuery query, CancellationToken cancellationToken)
        {
            var entity = await _transferRepository.GetByIdAsync(query.Id, cancellationToken);
            if (entity == null)
                throw new NotFoundException(ErrorMessages.TransferNotFound, ErrorCode.TransferNotFound);

            return entity.ToDto();
        }
    }
}