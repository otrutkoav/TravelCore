using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence.Hotels;
using TourCore.Application.Common.Errors;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Hotels.AccommodationPlacementRules.Mappings;
using TourCore.Application.Hotels.AccommodationPlacementRules.Queries;
using TourCore.Contracts.Hotels.AccommodationPlacementRules;

namespace TourCore.Application.Hotels.AccommodationPlacementRules.Handlers
{
    public class GetAccommodationPlacementRuleByIdHandler : IQueryHandler<GetAccommodationPlacementRuleByIdQuery, AccommodationPlacementRuleDto>
    {
        private readonly IAccommodationPlacementRuleRepository _repository;

        public GetAccommodationPlacementRuleByIdHandler(IAccommodationPlacementRuleRepository repository)
        {
            _repository = repository;
        }

        public async Task<AccommodationPlacementRuleDto> Handle(GetAccommodationPlacementRuleByIdQuery query, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(query.Id, cancellationToken);

            if (entity == null)
                throw new NotFoundException(ErrorMessages.AccommodationPlacementRuleNotFound, ErrorCode.AccommodationPlacementRuleNotFound);

            return entity.ToDto();
        }
    }
}