using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.RoomCategories.DTOs;
using TourCore.Application.RoomCategories.Mappings;
using TourCore.Application.RoomCategories.Queries;

namespace TourCore.Application.RoomCategories.Handlers
{
    public class GetRoomCategoryByIdHandler : IQueryHandler<GetRoomCategoryByIdQuery, RoomCategoryDto>
    {
        private readonly IRoomCategoryRepository _roomCategoryRepository;

        public GetRoomCategoryByIdHandler(IRoomCategoryRepository roomCategoryRepository)
        {
            _roomCategoryRepository = roomCategoryRepository;
        }

        public async Task<RoomCategoryDto> Handle(GetRoomCategoryByIdQuery query, CancellationToken cancellationToken)
        {
            var entity = await _roomCategoryRepository.GetByIdAsync(query.Id, cancellationToken);
            if (entity == null)
                throw new NotFoundException("Room category was not found.");

            return entity.ToDto();
        }
    }
}