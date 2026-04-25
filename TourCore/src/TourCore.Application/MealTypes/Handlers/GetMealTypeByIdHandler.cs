using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Common.Exceptions;
using TourCore.Contracts.Hotels.MealTypes;
using TourCore.Application.MealTypes.Mappings;
using TourCore.Application.MealTypes.Queries;

namespace TourCore.Application.MealTypes.Handlers
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
                throw new NotFoundException("Meal type was not found.");

            return entity.ToDto();
        }
    }
}