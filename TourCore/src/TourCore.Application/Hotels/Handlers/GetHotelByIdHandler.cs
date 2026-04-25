using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Hotels.Mappings;
using TourCore.Application.Hotels.Queries;
using TourCore.Contracts.Hotels.Hotels;

namespace TourCore.Application.Hotels.Handlers
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
                throw new NotFoundException("Hotel was not found.");

            return entity.ToDto();
        }
    }
}