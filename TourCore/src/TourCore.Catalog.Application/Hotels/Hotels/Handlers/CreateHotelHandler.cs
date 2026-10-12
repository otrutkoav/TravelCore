using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Abstractions.Persistence.Geography;
using TourCore.Application.Abstractions.Persistence.Hotels;
using TourCore.Application.Abstractions.Services;
using TourCore.Application.Common.Errors;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Hotels.Hotels.Commands;
using TourCore.Application.Hotels.Hotels.Mappings;
using TourCore.Application.Hotels.Hotels.Validators;
using TourCore.Contracts.Hotels.Hotels;
using TourCore.Domain.Hotels.Entities;

namespace TourCore.Application.Hotels.Hotels.Handlers
{
    public class CreateHotelHandler : ICommandHandler<CreateHotelCommand, HotelDto>
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IResortRepository _resortRepository;
        private readonly IHotelCategoryRepository _hotelCategoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly CreateHotelCommandValidator _validator;

        public CreateHotelHandler(
            IHotelRepository hotelRepository,
            ICountryRepository countryRepository,
            ICityRepository cityRepository,
            IResortRepository resortRepository,
            IHotelCategoryRepository hotelCategoryRepository,
            IUnitOfWork unitOfWork,
            IDateTimeProvider dateTimeProvider,
            CreateHotelCommandValidator validator)
        {
            _hotelRepository = hotelRepository;
            _countryRepository = countryRepository;
            _cityRepository = cityRepository;
            _resortRepository = resortRepository;
            _hotelCategoryRepository = hotelCategoryRepository;
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
            _validator = validator;
        }

        public async Task<HotelDto> Handle(CreateHotelCommand command, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(command);

            var normalizedCode = command.Code.Trim().ToUpperInvariant();

            if (await _hotelRepository.ExistsByCodeAsync(normalizedCode, cancellationToken))
                throw new ConflictException(ErrorMessages.HotelCodeExists, ErrorCode.HotelCodeExists);

            var country = await _countryRepository.GetByIdAsync(command.CountryId, cancellationToken);
            if (country == null)
                throw new NotFoundException(ErrorMessages.CountryNotFound, ErrorCode.CountryNotFound);

            var city = await _cityRepository.GetByIdAsync(command.CityId, cancellationToken);
            if (city == null)
                throw new NotFoundException(ErrorMessages.CityNotFound, ErrorCode.CityNotFound);

            if (city.CountryId != command.CountryId)
                throw new ValidationException(new System.Collections.Generic.Dictionary<string, string[]>
                {
                    { "CityId", new[] { ErrorCode.RegionCountryMismatch } }
                });

            if (command.ResortId.HasValue)
            {
                var resort = await _resortRepository.GetByIdAsync(command.ResortId.Value, cancellationToken);
                if (resort == null)
                    throw new NotFoundException(ErrorMessages.ResortNotFound, ErrorCode.ResortNotFound);
            }

            if (command.CategoryId.HasValue)
            {
                var category = await _hotelCategoryRepository.GetByIdAsync(command.CategoryId.Value, cancellationToken);
                if (category == null)
                    throw new NotFoundException(ErrorMessages.HotelCategoryNotFound, ErrorCode.HotelCategoryNotFound);
            }

            var entity = new Hotel(
                command.CountryId,
                command.CityId,
                command.Name,
                command.Stars,
                _dateTimeProvider.UtcNow,
                command.ResortId,
                command.CategoryId,
                command.NameEn,
                command.Code,
                command.Address,
                command.Phone,
                command.Fax,
                command.Email,
                command.Website,
                command.Latitude,
                command.Longitude,
                command.IsCruise,
                command.SortOrder,
                command.Rank);

            await _hotelRepository.AddAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return entity.ToDto();
        }
    }
}