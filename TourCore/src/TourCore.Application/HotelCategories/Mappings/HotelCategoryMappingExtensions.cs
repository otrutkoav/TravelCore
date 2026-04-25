using TourCore.Contracts.Hotels.HotelCategories;
using TourCore.Domain.Hotels.Entities;

namespace TourCore.Application.HotelCategories.Mappings
{
    public static class HotelCategoryMappingExtensions
    {
        public static HotelCategoryDto ToDto(this HotelCategory entity)
        {
            return new HotelCategoryDto
            {
                Id = entity.Id,
                Name = entity.Name,
                NameEn = entity.NameEn,
                PrintOrder = entity.PrintOrder,
                GlobalCode = entity.GlobalCode,
                Description = entity.Description
            };
        }

        public static HotelCategoryListItemDto ToListItemDto(this HotelCategory entity)
        {
            return new HotelCategoryListItemDto
            {
                Id = entity.Id,
                Name = entity.Name,
                NameEn = entity.NameEn,
                PrintOrder = entity.PrintOrder,
                GlobalCode = entity.GlobalCode,
                Description = entity.Description
            };
        }
    }
}