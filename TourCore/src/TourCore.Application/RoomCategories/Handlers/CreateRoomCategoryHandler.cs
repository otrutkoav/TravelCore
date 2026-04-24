using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Abstractions.Services;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.RoomCategories.Commands;
using TourCore.Application.RoomCategories.DTOs;
using TourCore.Application.RoomCategories.Mappings;
using TourCore.Application.RoomCategories.Validators;
using TourCore.Domain.Hotels.Entities;

namespace TourCore.Application.RoomCategories.Handlers
{
    public class CreateRoomCategoryHandler : ICommandHandler<CreateRoomCategoryCommand, RoomCategoryDto>
    {
        private readonly IRoomCategoryRepository _roomCategoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly CreateRoomCategoryCommandValidator _validator;

        public CreateRoomCategoryHandler(
            IRoomCategoryRepository roomCategoryRepository,
            IUnitOfWork unitOfWork,
            IDateTimeProvider dateTimeProvider,
            CreateRoomCategoryCommandValidator validator)
        {
            _roomCategoryRepository = roomCategoryRepository;
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
            _validator = validator;
        }

        public async Task<RoomCategoryDto> Handle(CreateRoomCategoryCommand command, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(command);

            var normalizedCode = command.Code.Trim().ToUpperInvariant();

            if (await _roomCategoryRepository.ExistsByCodeAsync(normalizedCode, cancellationToken))
                throw new ConflictException("Room category with same code already exists.");

            var entity = new RoomCategory(
                command.Name,
                _dateTimeProvider.UtcNow,
                normalizedCode,
                command.NameEn,
                command.SortOrder,
                command.Description);

            await _roomCategoryRepository.AddAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return entity.ToDto();
        }
    }
}