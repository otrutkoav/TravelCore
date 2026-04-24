using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.AccommodationTypes.DTOs;
using TourCore.Application.AccommodationTypes.Mappings;
using TourCore.Application.AccommodationTypes.Queries;
using TourCore.Application.Common.Exceptions;

namespace TourCore.Application.AccommodationTypes.Handlers
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