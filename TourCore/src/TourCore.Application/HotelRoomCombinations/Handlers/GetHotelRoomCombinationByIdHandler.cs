using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Common.Exceptions;
using TourCore.Contracts.Hotels.HotelRoomCombinations;
using TourCore.Application.HotelRoomCombinations.Mappings;
using TourCore.Application.HotelRoomCombinations.Queries;

namespace TourCore.Application.HotelRoomCombinations.Handlers
{
    public class GetHotelRoomCombinationByIdHandler : IQueryHandler<GetHotelRoomCombinationByIdQuery, HotelRoomCombinationDto>
    {
        private readonly IHotelRoomCombinationRepository _hotelRoomCombinationRepository;

        public GetHotelRoomCombinationByIdHandler(IHotelRoomCombinationRepository hotelRoomCombinationRepository)
        {
            _hotelRoomCombinationRepository = hotelRoomCombinationRepository;
        }

        public async Task<HotelRoomCombinationDto> Handle(GetHotelRoomCombinationByIdQuery query, CancellationToken cancellationToken)
        {
            var entity = await _hotelRoomCombinationRepository.GetByIdAsync(query.Id, cancellationToken);
            if (entity == null)
                throw new NotFoundException("Hotel room combination was not found.");

            return entity.ToDto();
        }
    }
}