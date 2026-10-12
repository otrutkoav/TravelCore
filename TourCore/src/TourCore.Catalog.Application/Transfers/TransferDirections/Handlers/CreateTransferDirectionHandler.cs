using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Abstractions.Services;
using TourCore.Contracts.Transfers.TransferDirections;
using TourCore.Domain.Transfers.Entities;
using TourCore.Application.Abstractions.Persistence.Transfers;
using TourCore.Application.Transfers.TransferDirections.Commands;
using TourCore.Application.Transfers.TransferDirections.Mappings;
using TourCore.Application.Transfers.TransferDirections.Validators;

namespace TourCore.Application.Transfers.TransferDirections.Handlers
{
    public class CreateTransferDirectionHandler : ICommandHandler<CreateTransferDirectionCommand, TransferDirectionDto>
    {
        private readonly ITransferDirectionRepository _transferDirectionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly CreateTransferDirectionCommandValidator _validator;

        public CreateTransferDirectionHandler(
            ITransferDirectionRepository transferDirectionRepository,
            IUnitOfWork unitOfWork,
            IDateTimeProvider dateTimeProvider,
            CreateTransferDirectionCommandValidator validator)
        {
            _transferDirectionRepository = transferDirectionRepository;
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
            _validator = validator;
        }

        public async Task<TransferDirectionDto> Handle(CreateTransferDirectionCommand command, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(command);

            var entity = new TransferDirection(
                command.Name,
                _dateTimeProvider.UtcNow);

            await _transferDirectionRepository.AddAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return entity.ToDto();
        }
    }
}