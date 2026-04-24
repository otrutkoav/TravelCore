using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Abstractions.Services;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.MealTypes.Commands;
using TourCore.Application.MealTypes.DTOs;
using TourCore.Application.MealTypes.Mappings;
using TourCore.Application.MealTypes.Validators;

namespace TourCore.Application.MealTypes.Handlers
{
    public class UpdateMealTypeHandler : ICommandHandler<UpdateMealTypeCommand, MealTypeDto>
    {
        private readonly IMealTypeRepository _mealTypeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly UpdateMealTypeCommandValidator _validator;

        public UpdateMealTypeHandler(
            IMealTypeRepository mealTypeRepository,
            IUnitOfWork unitOfWork,
            IDateTimeProvider dateTimeProvider,
            UpdateMealTypeCommandValidator validator)
        {
            _mealTypeRepository = mealTypeRepository;
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
            _validator = validator;
        }

        public async Task<MealTypeDto> Handle(UpdateMealTypeCommand command, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(command);

            var entity = await _mealTypeRepository.GetByIdAsync(command.Id, cancellationToken);
            if (entity == null)
                throw new NotFoundException("Meal type was not found.");

            var normalizedCode = command.Code.Trim().ToUpperInvariant();

            if (await _mealTypeRepository.ExistsByCodeAsync(normalizedCode, command.Id, cancellationToken))
                throw new ConflictException("Meal type with the same code already exists.");

            if (!string.IsNullOrWhiteSpace(command.GlobalCode))
            {
                var normalizedGlobalCode = command.GlobalCode.Trim().ToUpperInvariant();

                if (await _mealTypeRepository.ExistsByGlobalCodeAsync(normalizedGlobalCode, command.Id, cancellationToken))
                    throw new ConflictException("Meal type with the same global code already exists.");
            }

            entity.Update(
                command.Name,
                _dateTimeProvider.UtcNow,
                command.NameEn,
                command.Code,
                command.GlobalCode,
                command.SortOrder,
                command.Description);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return entity.ToDto();
        }
    }
}