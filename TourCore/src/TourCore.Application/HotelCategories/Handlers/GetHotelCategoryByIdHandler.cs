using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Common.Exceptions;
using TourCore.Contracts.Hotels.HotelCategories;
using TourCore.Application.HotelCategories.Mappings;
using TourCore.Application.HotelCategories.Queries;

namespace TourCore.Application.HotelCategories.Handlers
{
    public class GetHotelCategoryByIdHandler : IQueryHandler<GetHotelCategoryByIdQuery, HotelCategoryDto>
    {
        private readonly IHotelCategoryRepository _hotelCategoryRepository;

        public GetHotelCategoryByIdHandler(IHotelCategoryRepository hotelCategoryRepository)
        {
            _hotelCategoryRepository = hotelCategoryRepository;
        }

        public async Task<HotelCategoryDto> Handle(GetHotelCategoryByIdQuery query, CancellationToken cancellationToken)
        {
            var entity = await _hotelCategoryRepository.GetByIdAsync(query.Id, cancellationToken);
            if (entity == null)
                throw new NotFoundException("Hotel category was not found.");

            return entity.ToDto();
        }
    }
}