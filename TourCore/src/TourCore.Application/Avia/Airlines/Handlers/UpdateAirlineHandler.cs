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
                throw new NotFoundException("Airline was not found.");

            var normalizedCode = command.Code.Trim().ToUpperInvariant();

            if (await _airlineRepository.ExistsByCodeAsync(normalizedCode, command.Id, cancellationToken))
                throw new ConflictException("Airline with same code already exists.");

            if (!string.IsNullOrWhiteSpace(command.IcaoCode))
            {
                var normalizedIcaoCode = command.IcaoCode.Trim().ToUpperInvariant();

                if (await _airlineRepository.ExistsByIcaoCodeAsync(normalizedIcaoCode, command.Id, cancellationToken))
                    throw new ConflictException("Airline with same ICAO code already exists.");
            }

            entity.Update(
                normalizedCode,
                command.Name,
                _dateTimeProvider.UtcNow,
                command.NameEn,
                command.IcaoCode);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return entity.ToDto();
        }
    }
}