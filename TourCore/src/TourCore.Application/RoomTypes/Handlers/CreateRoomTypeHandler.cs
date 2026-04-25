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
using TourCore.Domain.Hotels.Entities;
using TourCore.Contracts.Hotels.RoomTypes;

namespace TourCore.Application.RoomTypes.Handlers
{
    public class CreateRoomTypeHandler : ICommandHandler<CreateRoomTypeCommand, RoomTypeDto>
    {
        private readonly IRoomTypeRepository _roomTypeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly CreateRoomTypeCommandValidator _validator;

        public CreateRoomTypeHandler(
            IRoomTypeRepository roomTypeRepository,
            IUnitOfWork unitOfWork,
            IDateTimeProvider dateTimeProvider,
            CreateRoomTypeCommandValidator validator)
        {
            _roomTypeRepository = roomTypeRepository;
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
            _validator = validator;
        }

        public async Task<RoomTypeDto> Handle(CreateRoomTypeCommand command, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(command);

            var normalizedCode = command.Code.Trim().ToUpperInvariant();

            if (await _roomTypeRepository.ExistsByCodeAsync(normalizedCode, cancellationToken))
                throw new ConflictException("Room type with same code already exists.");

            var entity = new RoomType(
                command.Name,
                _dateTimeProvider.UtcNow,
                normalizedCode,
                command.NameEn,
                command.Places,
                command.ExtraPlaces,
                command.SortOrder,
                command.Description);

            await _roomTypeRepository.AddAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return entity.ToDto();
        }
    }
}