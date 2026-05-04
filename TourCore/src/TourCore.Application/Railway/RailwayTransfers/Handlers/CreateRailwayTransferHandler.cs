using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Abstractions.Persistence.Geography;
using TourCore.Application.Abstractions.Persistence.Railway;
using TourCore.Application.Abstractions.Services;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Railway.RailwayTransfers.Commands;
using TourCore.Application.Railway.RailwayTransfers.Mappings;
using TourCore.Application.Railway.RailwayTransfers.Validators;
using TourCore.Contracts.Railway.RailwayTransfers;
using TourCore.Domain.Railway.Entities;
using TourCore.Application.Common.Errors;

namespace TourCore.Application.Railway.RailwayTransfers.Handlers
{
    public class CreateRailwayTransferHandler : ICommandHandler<CreateRailwayTransferCommand, RailwayTransferDto>
    {
        private readonly IRailwayTransferRepository _repository;
        private readonly ICountryRepository _countryRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly CreateRailwayTransferCommandValidator _validator;

        public CreateRailwayTransferHandler(
            IRailwayTransferRepository repository,
            ICountryRepository countryRepository,
            ICityRepository cityRepository,
            IUnitOfWork unitOfWork,
            IDateTimeProvider dateTimeProvider,
            CreateRailwayTransferCommandValidator validator)
        {
            _repository = repository;
            _countryRepository = countryRepository;
            _cityRepository = cityRepository;
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
            _validator = validator;
        }

        public async Task<RailwayTransferDto> Handle(CreateRailwayTransferCommand command, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(command);

            var countryFrom = await _countryRepository.GetByIdAsync(command.CountryFromId, cancellationToken)
                ?? throw new NotFoundException(ErrorMessages.DepartureCountryNotFound, ErrorCode.DepartureCountryNotFound);

            var cityFrom = await _cityRepository.GetByIdAsync(command.CityFromId, cancellationToken)
                ?? throw new NotFoundException(ErrorMessages.DepartureCityNotFound, ErrorCode.DepartureCityNotFound);

            var countryTo = await _countryRepository.GetByIdAsync(command.CountryToId, cancellationToken)
                ?? throw new NotFoundException(ErrorMessages.ArrivalCountryNotFound, ErrorCode.ArrivalCountryNotFound);

            var cityTo = await _cityRepository.GetByIdAsync(command.CityToId, cancellationToken)
                ?? throw new NotFoundException(ErrorMessages.ArrivalCityNotFound, ErrorCode.ArrivalCityNotFound);

            if (cityFrom.CountryId != command.CountryFromId)
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    { "CityFromId", new[] { ErrorCode.DepartureCityCountryMismatch } }
                });

            if (cityTo.CountryId != command.CountryToId)
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    { "CityToId", new[] { ErrorCode.ArrivalCityCountryMismatch } }
                });

            var entity = new RailwayTransfer(
                command.Name,
                command.CountryFromId,
                command.CityFromId,
                command.CountryToId,
                command.CityToId,
                _dateTimeProvider.UtcNow);

            await _repository.AddAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return entity.ToDto();
        }
    }
}