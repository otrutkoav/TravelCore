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

namespace TourCore.Application.Railway.RailwayTransfers.Handlers
{
    public class UpdateRailwayTransferHandler : ICommandHandler<UpdateRailwayTransferCommand, RailwayTransferDto>
    {
        private readonly IRailwayTransferRepository _railwayTransferRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly UpdateRailwayTransferCommandValidator _validator;

        public UpdateRailwayTransferHandler(
            IRailwayTransferRepository railwayTransferRepository,
            ICountryRepository countryRepository,
            ICityRepository cityRepository,
            IUnitOfWork unitOfWork,
            IDateTimeProvider dateTimeProvider,
            UpdateRailwayTransferCommandValidator validator)
        {
            _railwayTransferRepository = railwayTransferRepository;
            _countryRepository = countryRepository;
            _cityRepository = cityRepository;
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
            _validator = validator;
        }

        public async Task<RailwayTransferDto> Handle(UpdateRailwayTransferCommand command, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(command);

            var entity = await _railwayTransferRepository.GetByIdAsync(command.Id, cancellationToken);
            if (entity == null)
                throw new NotFoundException(ErrorMessages.RailwayTransferNotFound, ErrorCode.RailwayTransferNotFound);

            var countryFrom = await _countryRepository.GetByIdAsync(command.CountryFromId, cancellationToken);
            if (countryFrom == null)
                throw new NotFoundException(ErrorMessages.DepartureCountryNotFound, ErrorCode.DepartureCountryNotFound);

            var cityFrom = await _cityRepository.GetByIdAsync(command.CityFromId, cancellationToken);
            if (cityFrom == null)
                throw new NotFoundException(ErrorMessages.DepartureCityNotFound, ErrorCode.DepartureCityNotFound);

            var countryTo = await _countryRepository.GetByIdAsync(command.CountryToId, cancellationToken);
            if (countryTo == null)
                throw new NotFoundException(ErrorMessages.ArrivalCountryNotFound, ErrorCode.ArrivalCountryNotFound);

            var cityTo = await _cityRepository.GetByIdAsync(command.CityToId, cancellationToken);
            if (cityTo == null)
                throw new NotFoundException(ErrorMessages.ArrivalCityNotFound, ErrorCode.ArrivalCityNotFound);

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

            entity.Update(
                command.Name,
                command.CountryFromId,
                command.CityFromId,
                command.CountryToId,
                command.CityToId,
                _dateTimeProvider.UtcNow);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return entity.ToDto();
        }
    }
}