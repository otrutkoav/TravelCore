using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Common.Exceptions;
using TourCore.Contracts.Hotels.RoomCategories;
using TourCore.Application.RoomTypes.Mappings;
using TourCore.Application.RoomTypes.Queries;
using TourCore.Contracts.Hotels.RoomTypes;

namespace TourCore.Application.RoomTypes.Handlers
{
    public class GetRoomTypeByIdHandler : IQueryHandler<GetRoomTypeByIdQuery, RoomTypeDto>
    {
        private readonly IRoomTypeRepository _roomTypeRepository;

        public GetRoomTypeByIdHandler(IRoomTypeRepository roomTypeRepository)
        {
            _roomTypeRepository = roomTypeRepository;
        }

        public async Task<RoomTypeDto> Handle(GetRoomTypeByIdQuery query, CancellationToken cancellationToken)
        {
            var entity = await _roomTypeRepository.GetByIdAsync(query.Id, cancellationToken);
            if (entity == null)
                throw new NotFoundException("Room type was not found.");

            return entity.ToDto();
        }
    }
}