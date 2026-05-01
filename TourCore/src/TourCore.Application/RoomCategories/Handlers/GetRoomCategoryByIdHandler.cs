using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Common.Errors;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.RoomCategories.Mappings;
using TourCore.Application.RoomCategories.Queries;
using TourCore.Contracts.Hotels.RoomCategories;

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
                throw new NotFoundException(ErrorMessages.RoomCategoryNotFound, ErrorCode.RoomCategoryNotFound);

            return entity.ToDto();
        }
    }
}