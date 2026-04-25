using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Abstractions.Services;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.MealTypes.Commands;
using TourCore.Contracts.Hotels.MealTypes;
using TourCore.Application.MealTypes.Mappings;
using TourCore.Application.MealTypes.Validators;
using TourCore.Domain.Hotels.Entities;

namespace TourCore.Application.MealTypes.Handlers
{
    public class CreateMealTypeHandler : ICommandHandler<CreateMealTypeCommand, MealTypeDto>
    {
        private readonly IMealTypeRepository _mealTypeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly CreateMealTypeCommandValidator _validator;

        public CreateMealTypeHandler(
            IMealTypeRepository mealTypeRepository,
            IUnitOfWork unitOfWork,
            IDateTimeProvider dateTimeProvider,
            CreateMealTypeCommandValidator validator)
        {
            _mealTypeRepository = mealTypeRepository;
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
            _validator = validator;
        }

        public async Task<MealTypeDto> Handle(CreateMealTypeCommand command, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(command);

            var normalizedCode = command.Code.Trim().ToUpperInvariant();

            if (await _mealTypeRepository.ExistsByCodeAsync(normalizedCode, cancellationToken))
                throw new ConflictException("Meal type with the same code already exists.");

            if (!string.IsNullOrWhiteSpace(command.GlobalCode))
            {
                var normalizedGlobalCode = command.GlobalCode.Trim().ToUpperInvariant();

                if (await _mealTypeRepository.ExistsByGlobalCodeAsync(normalizedGlobalCode, cancellationToken))
                    throw new ConflictException("Meal type with the same global code already exists.");
            }

            var entity = new MealType(
                command.Name,
                _dateTimeProvider.UtcNow,
                command.NameEn,
                command.Code,
                command.GlobalCode,
                command.SortOrder,
                command.Description);

            await _mealTypeRepository.AddAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return entity.ToDto();
        }
    }
}