using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Abstractions.Services;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.RoomTypes.Commands;
using TourCore.Contracts.Hotels.RoomCategories;
using TourCore.Application.RoomTypes.Mappings;
using TourCore.Application.RoomTypes.Validators;
using TourCore.Contracts.Hotels.RoomTypes;

namespace TourCore.Application.RoomTypes.Handlers
{
    public class UpdateRoomTypeHandler : ICommandHandler<UpdateRoomTypeCommand, RoomTypeDto>
    {
        private readonly IRoomTypeRepository _roomTypeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly UpdateRoomTypeCommandValidator _validator;

        public UpdateRoomTypeHandler(
            IRoomTypeRepository roomTypeRepository,
            IUnitOfWork unitOfWork,
            IDateTimeProvider dateTimeProvider,
            UpdateRoomTypeCommandValidator validator)
        {
            _roomTypeRepository = roomTypeRepository;
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
            _validator = validator;
        }

        public async Task<RoomTypeDto> Handle(UpdateRoomTypeCommand command, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(command);

            var entity = await _roomTypeRepository.GetByIdAsync(command.Id, cancellationToken);
            if (entity == null)
                throw new NotFoundException("Room type was not found.");

            var normalizedCode = command.Code.Trim().ToUpperInvariant();

            if (await _roomTypeRepository.ExistsByCodeAsync(normalizedCode, command.Id, cancellationToken))
                throw new ConflictException("Room type with same code already exists.");

            entity.Update(
                command.Name,
                _dateTimeProvider.UtcNow,
                normalizedCode,
                command.NameEn,
                command.Places,
                command.ExtraPlaces,
                command.SortOrder,
                command.Description);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return entity.ToDto();
        }
    }
}