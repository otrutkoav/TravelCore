using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Abstractions.Persistence.Hotels;
using TourCore.Application.Abstractions.Services;
using TourCore.Application.Common.Errors;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Hotels.AccommodationPlacementRules.Commands;
using TourCore.Application.Hotels.AccommodationPlacementRules.Mappings;
using TourCore.Application.Hotels.AccommodationPlacementRules.Validators;
using TourCore.Contracts.Hotels.AccommodationPlacementRules;

namespace TourCore.Application.Hotels.AccommodationPlacementRules.Handlers
{
    public class UpdateAccommodationPlacementRuleHandler : ICommandHandler<UpdateAccommodationPlacementRuleCommand, AccommodationPlacementRuleDto>
    {
        private readonly IAccommodationPlacementRuleRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly UpdateAccommodationPlacementRuleCommandValidator _validator;

        public UpdateAccommodationPlacementRuleHandler(
            IAccommodationPlacementRuleRepository repository,
            IUnitOfWork unitOfWork,
            IDateTimeProvider dateTimeProvider,
            UpdateAccommodationPlacementRuleCommandValidator validator)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
            _validator = validator;
        }

        public async Task<AccommodationPlacementRuleDto> Handle(UpdateAccommodationPlacementRuleCommand command, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(command);

            var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);

            if (entity == null)
                throw new NotFoundException(ErrorMessages.AccommodationPlacementRuleNotFound, ErrorCode.AccommodationPlacementRuleNotFound);

            entity.Update(
                command.AdultsCount,
                command.ChildrenCount,
                command.ChildrenAreInfants,
                _dateTimeProvider.UtcNow);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return entity.ToDto();
        }
    }
}