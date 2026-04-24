using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Abstractions.Services;
using TourCore.Application.Airlines.Commands;
using TourCore.Contracts.Avia.Airlines;
using TourCore.Application.Airlines.Mappings;
using TourCore.Application.Airlines.Validators;
using TourCore.Application.Common.Exceptions;
using TourCore.Domain.Avia.Entities;

namespace TourCore.Application.Airlines.Handlers
{
    public class CreateAirlineHandler : ICommandHandler<CreateAirlineCommand, AirlineDto>
    {
        private readonly IAirlineRepository _airlineRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly CreateAirlineCommandValidator _validator;

        public CreateAirlineHandler(
            IAirlineRepository airlineRepository,
            IUnitOfWork unitOfWork,
            IDateTimeProvider dateTimeProvider,
            CreateAirlineCommandValidator validator)
        {
            _airlineRepository = airlineRepository;
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
            _validator = validator;
        }

        public async Task<AirlineDto> Handle(CreateAirlineCommand command, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(command);

            var normalizedCode = command.Code.Trim().ToUpperInvariant();

            if (await _airlineRepository.ExistsByCodeAsync(normalizedCode, cancellationToken))
                throw new ConflictException("Airline with same code already exists.");

            if (!string.IsNullOrWhiteSpace(command.IcaoCode))
            {
                var normalizedIcaoCode = command.IcaoCode.Trim().ToUpperInvariant();

                if (await _airlineRepository.ExistsByIcaoCodeAsync(normalizedIcaoCode, cancellationToken))
                    throw new ConflictException("Airline with same ICAO code already exists.");
            }

            var entity = new Airline(
                normalizedCode,
                command.Name,
                _dateTimeProvider.UtcNow,
                command.NameEn,
                command.IcaoCode);

            await _airlineRepository.AddAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return entity.ToDto();
        }
    }
}