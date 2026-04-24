using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Abstractions.Services;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Resorts.Commands;
using TourCore.Contracts.Geography.Resorts;
using TourCore.Application.Resorts.Mappings;
using TourCore.Application.Resorts.Validators;

namespace TourCore.Application.Resorts.Handlers
{
    public class UpdateResortHandler : ICommandHandler<UpdateResortCommand, ResortDto>
    {
        private readonly IResortRepository _resortRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly UpdateResortCommandValidator _validator;

        public UpdateResortHandler(
            IResortRepository resortRepository,
            ICountryRepository countryRepository,
            IUnitOfWork unitOfWork,
            IDateTimeProvider dateTimeProvider,
            UpdateResortCommandValidator validator)
        {
            _resortRepository = resortRepository;
            _countryRepository = countryRepository;
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
            _validator = validator;
        }

        public async Task<ResortDto> Handle(UpdateResortCommand command, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(command);

            var entity = await _resortRepository.GetByIdAsync(command.Id, cancellationToken);
            if (entity == null)
                throw new NotFoundException("Resort was not found.");

            if (!await _countryRepository.ExistsAsync(command.CountryId, cancellationToken))
                throw new NotFoundException("Country was not found.");

            entity.Update(
                command.CountryId,
                command.Name,
                _dateTimeProvider.UtcNow,
                command.NameEn);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return entity.ToDto();
        }
    }
}