using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence.Hotels;
using TourCore.Application.Common.Errors;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Hotels.MealTypes.Mappings;
using TourCore.Application.Hotels.MealTypes.Queries;
using TourCore.Contracts.Hotels.MealTypes;

namespace TourCore.Application.Hotels.MealTypes.Handlers
{
    public class GetMealTypeByIdHandler : IQueryHandler<GetMealTypeByIdQuery, MealTypeDto>
    {
        private readonly IMealTypeRepository _mealTypeRepository;

        public GetMealTypeByIdHandler(IMealTypeRepository mealTypeRepository)
        {
            _mealTypeRepository = mealTypeRepository;
        }

        public async Task<MealTypeDto> Handle(GetMealTypeByIdQuery query, CancellationToken cancellationToken)
        {
            var entity = await _mealTypeRepository.GetByIdAsync(query.Id, cancellationToken);
            if (entity == null)
                throw new NotFoundException(ErrorMessages.MealTypeNotFound, ErrorCode.MealTypeNotFound);

            return entity.ToDto();
        }
    }
}