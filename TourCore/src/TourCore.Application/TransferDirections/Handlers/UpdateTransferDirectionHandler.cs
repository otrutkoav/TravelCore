using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Abstractions.Services;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.TransferDirections.Commands;
using TourCore.Contracts.Transfers.TransferDirections;
using TourCore.Application.TransferDirections.Mappings;
using TourCore.Application.TransferDirections.Validators;

namespace TourCore.Application.TransferDirections.Handlers
{
    public class UpdateTransferDirectionHandler : ICommandHandler<UpdateTransferDirectionCommand, TransferDirectionDto>
    {
        private readonly ITransferDirectionRepository _transferDirectionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly UpdateTransferDirectionCommandValidator _validator;

        public UpdateTransferDirectionHandler(
            ITransferDirectionRepository transferDirectionRepository,
            IUnitOfWork unitOfWork,
            IDateTimeProvider dateTimeProvider,
            UpdateTransferDirectionCommandValidator validator)
        {
            _transferDirectionRepository = transferDirectionRepository;
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
            _validator = validator;
        }

        public async Task<TransferDirectionDto> Handle(UpdateTransferDirectionCommand command, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(command);

            var entity = await _transferDirectionRepository.GetByIdAsync(command.Id, cancellationToken);
            if (entity == null)
                throw new NotFoundException("Transfer direction was not found.");

            entity.Update(
                command.Name,
                _dateTimeProvider.UtcNow);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return entity.ToDto();
        }
    }
}