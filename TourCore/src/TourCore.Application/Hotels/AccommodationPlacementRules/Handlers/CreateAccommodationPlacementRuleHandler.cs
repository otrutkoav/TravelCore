using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Abstractions.Persistence.Hotels;
using TourCore.Application.Abstractions.Services;
using TourCore.Application.Hotels.AccommodationPlacementRules.Commands;
using TourCore.Application.Hotels.AccommodationPlacementRules.Mappings;
using TourCore.Application.Hotels.AccommodationPlacementRules.Validators;
using TourCore.Contracts.Hotels.AccommodationPlacementRules;
using TourCore.Domain.Hotels.Entities;

namespace TourCore.Application.Hotels.AccommodationPlacementRules.Handlers
{
    public class CreateAccommodationPlacementRuleHandler : ICommandHandler<CreateAccommodationPlacementRuleCommand, AccommodationPlacementRuleDto>
    {
        private readonly IAccommodationPlacementRuleRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly CreateAccommodationPlacementRuleCommandValidator _validator;

        public CreateAccommodationPlacementRuleHandler(
            IAccommodationPlacementRuleRepository repository,
            IUnitOfWork unitOfWork,
            IDateTimeProvider dateTimeProvider,
            CreateAccommodationPlacementRuleCommandValidator validator)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
            _validator = validator;
        }

        public async Task<AccommodationPlacementRuleDto> Handle(CreateAccommodationPlacementRuleCommand command, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(command);

            var entity = new AccommodationPlacementRule(
                command.AdultsCount,
                command.ChildrenCount,
                command.ChildrenAreInfants,
                _dateTimeProvider.UtcNow);

            await _repository.AddAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return entity.ToDto();
        }
    }
}