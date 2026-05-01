using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Abstractions.Services;
using TourCore.Contracts.Avia.AirClasses;
using TourCore.Application.Common.Exceptions;
using TourCore.Domain.Avia.Entities;
using TourCore.Application.Abstractions.Persistence.Avia;
using TourCore.Application.Avia.AirClasses.Commands;
using TourCore.Application.Avia.AirClasses.Mappings;
using TourCore.Application.Avia.AirClasses.Validators;

namespace TourCore.Application.Avia.AirClasses.Handlers
{
    public class CreateAirClassHandler : ICommandHandler<CreateAirClassCommand, AirClassDto>
    {
        private readonly IAirClassRepository _airClassRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly CreateAirClassCommandValidator _validator;

        public CreateAirClassHandler(
            IAirClassRepository airClassRepository,
            IUnitOfWork unitOfWork,
            IDateTimeProvider dateTimeProvider,
            CreateAirClassCommandValidator validator)
        {
            _airClassRepository = airClassRepository;
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
            _validator = validator;
        }

        public async Task<AirClassDto> Handle(CreateAirClassCommand command, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(command);

            var normalizedCode = command.Code.Trim().ToUpperInvariant();

            if (await _airClassRepository.ExistsByCodeAsync(normalizedCode, cancellationToken))
                throw new ConflictException("Air class with same code already exists.");

            var entity = new AirClass(
                normalizedCode,
                command.Name,
                _dateTimeProvider.UtcNow,
                command.NameEn,
                command.Group,
                command.SortOrder);

            await _airClassRepository.AddAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return entity.ToDto();
        }
    }
}