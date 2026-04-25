using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Abstractions.Services;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.RoomCategories.Commands;
using TourCore.Contracts.Hotels.RoomCategories;
using TourCore.Application.RoomCategories.Mappings;
using TourCore.Application.RoomCategories.Validators;

namespace TourCore.Application.RoomCategories.Handlers
{
    public class UpdateRoomCategoryHandler : ICommandHandler<UpdateRoomCategoryCommand, RoomCategoryDto>
    {
        private readonly IRoomCategoryRepository _roomCategoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly UpdateRoomCategoryCommandValidator _validator;

        public UpdateRoomCategoryHandler(
            IRoomCategoryRepository roomCategoryRepository,
            IUnitOfWork unitOfWork,
            IDateTimeProvider dateTimeProvider,
            UpdateRoomCategoryCommandValidator validator)
        {
            _roomCategoryRepository = roomCategoryRepository;
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
            _validator = validator;
        }

        public async Task<RoomCategoryDto> Handle(UpdateRoomCategoryCommand command, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(command);

            var entity = await _roomCategoryRepository.GetByIdAsync(command.Id, cancellationToken);
            if (entity == null)
                throw new NotFoundException("Room category was not found.");

            var normalizedCode = command.Code.Trim().ToUpperInvariant();

            if (await _roomCategoryRepository.ExistsByCodeAsync(normalizedCode, command.Id, cancellationToken))
                throw new ConflictException("Room category with same code already exists.");

            entity.Update(
                command.Name,
                _dateTimeProvider.UtcNow,
                normalizedCode,
                command.NameEn,
                command.SortOrder,
                command.Description);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return entity.ToDto();
        }
    }
}