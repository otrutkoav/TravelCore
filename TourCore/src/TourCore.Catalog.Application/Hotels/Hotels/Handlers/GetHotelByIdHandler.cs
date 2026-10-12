using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence.Hotels;
using TourCore.Application.Common.Errors;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Hotels.Hotels.Mappings;
using TourCore.Application.Hotels.Hotels.Queries;
using TourCore.Contracts.Hotels.Hotels;

namespace TourCore.Application.Hotels.Hotels.Handlers
{
    public class GetHotelByIdHandler : IQueryHandler<GetHotelByIdQuery, HotelDto>
    {
        private readonly IHotelRepository _hotelRepository;

        public GetHotelByIdHandler(IHotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }

        public async Task<HotelDto> Handle(GetHotelByIdQuery query, CancellationToken cancellationToken)
        {
            var entity = await _hotelRepository.GetByIdAsync(query.Id, cancellationToken);

            if (entity == null)
                throw new NotFoundException(ErrorMessages.HotelNotFound, ErrorCode.HotelNotFound);

            return entity.ToDto();
        }
    }
}