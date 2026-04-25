using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Abstractions.Services;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Hotels.Commands;
using TourCore.Application.Hotels.Mappings;
using TourCore.Application.Hotels.Validators;
using TourCore.Contracts.Hotels.Hotels;

namespace TourCore.Application.Hotels.Handlers
{
    public class UpdateHotelHandler : ICommandHandler<UpdateHotelCommand, HotelDto>
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IResortRepository _resortRepository;
        private readonly IHotelCategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly UpdateHotelCommandValidator _validator;

        public UpdateHotelHandler(
            IHotelRepository hotelRepository,
            ICountryRepository countryRepository,
            ICityRepository cityRepository,
            IResortRepository resortRepository,
            IHotelCategoryRepository categoryRepository,
            IUnitOfWork unitOfWork,
            IDateTimeProvider dateTimeProvider,
            UpdateHotelCommandValidator validator)
        {
            _hotelRepository = hotelRepository;
            _countryRepository = countryRepository;
            _cityRepository = cityRepository;
            _resortRepository = resortRepository;
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
            _validator = validator;
        }

        public async Task<HotelDto> Handle(UpdateHotelCommand command, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(command);

            var entity = await _hotelRepository.GetByIdAsync(command.Id, cancellationToken);
            if (entity == null)
                throw new NotFoundException("Hotel was not found.");

            if (await _hotelRepository.ExistsByCodeAsync(command.Code, command.Id, cancellationToken))
                throw new ValidationException("Hotel with the same code already exists.");

            if (await _countryRepository.GetByIdAsync(command.CountryId, cancellationToken) == null)
                throw new NotFoundException("Country was not found.");

            if (await _cityRepository.GetByIdAsync(command.CityId, cancellationToken) == null)
                throw new NotFoundException("City was not found.");

            if (command.ResortId.HasValue && await _resortRepository.GetByIdAsync(command.ResortId.Value, cancellationToken) == null)
                throw new NotFoundException("Resort was not found.");

            if (command.CategoryId.HasValue && await _categoryRepository.GetByIdAsync(command.CategoryId.Value, cancellationToken) == null)
                throw new NotFoundException("Hotel category was not found.");

            entity.Update(
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

            _hotelRepository.Update(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return entity.ToDto();
        }
    }
}