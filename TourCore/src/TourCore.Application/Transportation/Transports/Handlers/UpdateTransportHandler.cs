using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Abstractions.Persistence.Transportation;
using TourCore.Application.Abstractions.Services;
using TourCore.Application.Common.Errors;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Transportation.Transports.Commands;
using TourCore.Application.Transportation.Transports.Mappings;
using TourCore.Application.Transportation.Transports.Validators;
using TourCore.Contracts.Transportation.Transports;

namespace TourCore.Application.Transportation.Transports.Handlers
{
    public class UpdateTransportHandler : ICommandHandler<UpdateTransportCommand, TransportDto>
    {
        private readonly ITransportRepository _transportRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly UpdateTransportCommandValidator _validator;

        public UpdateTransportHandler(
            ITransportRepository transportRepository,
            IUnitOfWork unitOfWork,
            IDateTimeProvider dateTimeProvider,
            UpdateTransportCommandValidator validator)
        {
            _transportRepository = transportRepository;
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
            _validator = validator;
        }

        public async Task<TransportDto> Handle(UpdateTransportCommand command, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(command);

            var entity = await _transportRepository.GetByIdAsync(command.Id, cancellationToken);
            if (entity == null)
                throw new NotFoundException(ErrorMessages.TransportNotFound, ErrorCode.TransportNotFound);

            entity.Update(
                command.Name,
                _dateTimeProvider.UtcNow,
                command.NameEn,
                command.SeatsCount,
                command.ShowOrder);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return entity.ToDto();
        }
    }
}