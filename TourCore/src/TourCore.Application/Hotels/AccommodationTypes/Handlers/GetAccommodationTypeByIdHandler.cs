using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Contracts.Hotels.AccommodationTypes;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Abstractions.Persistence.Hotels;
using TourCore.Application.Hotels.AccommodationTypes.Mappings;
using TourCore.Application.Hotels.AccommodationTypes.Queries;

namespace TourCore.Application.Hotels.AccommodationTypes.Handlers
{
    public class GetAccommodationTypeByIdHandler : IQueryHandler<GetAccommodationTypeByIdQuery, AccommodationTypeDto>
    {
        private readonly IAccommodationTypeRepository _accommodationTypeRepository;

        public GetAccommodationTypeByIdHandler(IAccommodationTypeRepository accommodationTypeRepository)
        {
            _accommodationTypeRepository = accommodationTypeRepository;
        }

        public async Task<AccommodationTypeDto> Handle(GetAccommodationTypeByIdQuery query, CancellationToken cancellationToken)
        {
            var entity = await _accommodationTypeRepository.GetByIdAsync(query.Id, cancellationToken);
            if (entity == null)
                throw new NotFoundException("Accommodation type was not found.");

            return entity.ToDto();
        }
    }
}