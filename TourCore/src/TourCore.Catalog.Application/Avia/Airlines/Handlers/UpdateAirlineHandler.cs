using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Abstractions.Services;
using TourCore.Contracts.Avia.Airlines;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Abstractions.Persistence.Avia;
using TourCore.Application.Avia.Airlines.Commands;
using TourCore.Application.Avia.Airlines.Mappings;
using TourCore.Application.Avia.Airlines.Validators;
using TourCore.Application.Common.Errors;

namespace TourCore.Application.Avia.Airlines.Handlers
{
    public class UpdateAirlineHandler : ICommandHandler<UpdateAirlineCommand, AirlineDto>
    {
        private readonly IAirlineRepository _airlineRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly UpdateAirlineCommandValidator _validator;

        public UpdateAirlineHandler(
            IAirlineRepository airlineRepository,
            IUnitOfWork unitOfWork,
            IDateTimeProvider dateTimeProvider,
            UpdateAirlineCommandValidator validator)
        {
            _airlineRepository = airlineRepository;
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
            _validator = validator;
        }

        public async Task<AirlineDto> Handle(UpdateAirlineCommand command, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(command);

            var entity = await _airlineRepository.GetByIdAsync(command.Id, cancellationToken);
            if (entity == null)
                throw new NotFoundException(ErrorMessages.AirlineNotFound, ErrorCode.AirlineNotFound);

            var normalizedCode = command.Code.Trim().ToUpperInvariant();

            if (await _airlineRepository.ExistsByCodeAsync(normalizedCode, command.Id, cancellationToken))
                throw new ConflictException(ErrorMessages.AirlineCodeExists, ErrorCode.AirlineCodeExists);

            string normalizedIcaoCode = null;

            if (!string.IsNullOrWhiteSpace(command.IcaoCode))
            {
                normalizedIcaoCode = command.IcaoCode.Trim().ToUpperInvariant();

                if (await _airlineRepository.ExistsByIcaoCodeAsync(normalizedIcaoCode, command.Id, cancellationToken))
                    throw new ConflictException(ErrorMessages.AirlineIcaoCodeExists, ErrorCode.AirlineIcaoCodeExists);
            }

            entity.Update(
                normalizedCode,
                command.Name,
                _dateTimeProvider.UtcNow,
                command.NameEn,
                normalizedIcaoCode);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return entity.ToDto();
        }
    }
}