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
using TourCore.Domain.Geography.Entities;

namespace TourCore.Application.Resorts.Handlers
{
    public class CreateResortHandler : ICommandHandler<CreateResortCommand, ResortDto>
    {
        private readonly IResortRepository _resortRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly CreateResortCommandValidator _validator;

        public CreateResortHandler(
            IResortRepository resortRepository,
            ICountryRepository countryRepository,
            IUnitOfWork unitOfWork,
            IDateTimeProvider dateTimeProvider,
            CreateResortCommandValidator validator)
        {
            _resortRepository = resortRepository;
            _countryRepository = countryRepository;
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
            _validator = validator;
        }

        public async Task<ResortDto> Handle(CreateResortCommand command, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(command);

            if (!await _countryRepository.ExistsAsync(command.CountryId, cancellationToken))
                throw new NotFoundException("Country was not found.");

            var entity = new Resort(
                command.CountryId,
                command.Name,
                _dateTimeProvider.UtcNow,
                command.NameEn);

            await _resortRepository.AddAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return entity.ToDto();
        }
    }
}