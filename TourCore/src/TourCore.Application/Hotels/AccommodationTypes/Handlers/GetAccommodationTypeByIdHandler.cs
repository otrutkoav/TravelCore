using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence.Hotels;
using TourCore.Application.Common.Errors;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Hotels.AccommodationTypes.Mappings;
using TourCore.Application.Hotels.AccommodationTypes.Queries;
using TourCore.Contracts.Hotels.AccommodationTypes;

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
                throw new NotFoundException(ErrorMessages.AccommodationTypeNotFound, ErrorCode.AccommodationTypeNotFound);

            return entity.ToDto();
        }
    }
}