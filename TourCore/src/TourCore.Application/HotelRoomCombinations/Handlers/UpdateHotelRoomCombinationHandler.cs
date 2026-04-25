using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Abstractions.Services;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.HotelRoomCombinations.Commands;
using TourCore.Contracts.Hotels.HotelRoomCombinations;
using TourCore.Application.HotelRoomCombinations.Mappings;
using TourCore.Application.HotelRoomCombinations.Validators;

namespace TourCore.Application.HotelRoomCombinations.Handlers
{
    public class UpdateHotelRoomCombinationHandler : ICommandHandler<UpdateHotelRoomCombinationCommand, HotelRoomCombinationDto>
    {
        private readonly IHotelRoomCombinationRepository _hotelRoomCombinationRepository;
        private readonly IRoomTypeRepository _roomTypeRepository;
        private readonly IRoomCategoryRepository _roomCategoryRepository;
        private readonly IAccommodationTypeRepository _accommodationTypeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly UpdateHotelRoomCombinationCommandValidator _validator;

        public UpdateHotelRoomCombinationHandler(
            IHotelRoomCombinationRepository hotelRoomCombinationRepository,
            IRoomTypeRepository roomTypeRepository,
            IRoomCategoryRepository roomCategoryRepository,
            IAccommodationTypeRepository accommodationTypeRepository,
            IUnitOfWork unitOfWork,
            IDateTimeProvider dateTimeProvider,
            UpdateHotelRoomCombinationCommandValidator validator)
        {
            _hotelRoomCombinationRepository = hotelRoomCombinationRepository;
            _roomTypeRepository = roomTypeRepository;
            _roomCategoryRepository = roomCategoryRepository;
            _accommodationTypeRepository = accommodationTypeRepository;
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
            _validator = validator;
        }

        public async Task<HotelRoomCombinationDto> Handle(UpdateHotelRoomCombinationCommand command, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(command);

            var entity = await _hotelRoomCombinationRepository.GetByIdAsync(command.Id, cancellationToken);
            if (entity == null)
                throw new NotFoundException("Hotel room combination was not found.");

            var roomType = await _roomTypeRepository.GetByIdAsync(command.RoomTypeId, cancellationToken);
            if (roomType == null)
                throw new NotFoundException("Room type was not found.");

            var roomCategory = await _roomCategoryRepository.GetByIdAsync(command.RoomCategoryId, cancellationToken);
            if (roomCategory == null)
                throw new NotFoundException("Room category was not found.");

            var accommodationType = await _accommodationTypeRepository.GetByIdAsync(command.AccommodationTypeId, cancellationToken);
            if (accommodationType == null)
                throw new NotFoundException("Accommodation type was not found.");

            if (await _hotelRoomCombinationRepository.ExistsAsync(
                    command.RoomTypeId,
                    command.RoomCategoryId,
                    command.AccommodationTypeId,
                    command.Id,
                    cancellationToken))
            {
                throw new ConflictException("Hotel room combination already exists.");
            }

            entity.Update(
                command.RoomTypeId,
                command.RoomCategoryId,
                command.AccommodationTypeId,
                _dateTimeProvider.UtcNow,
                command.IsMain,
                command.AgeFrom,
                command.AgeTo);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return entity.ToDto();
        }
    }
}