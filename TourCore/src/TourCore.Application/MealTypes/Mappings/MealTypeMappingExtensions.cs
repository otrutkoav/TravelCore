using TourCore.Contracts.Hotels.MealTypes;
using TourCore.Domain.Hotels.Entities;

namespace TourCore.Application.MealTypes.Mappings
{
    public static class MealTypeMappingExtensions
    {
        public static MealTypeDto ToDto(this MealType entity)
        {
            return new MealTypeDto
            {
                Id = entity.Id,
                Name = entity.Name,
                NameEn = entity.NameEn,
                Code = entity.Code,
                GlobalCode = entity.GlobalCode,
                SortOrder = entity.SortOrder,
                Description = entity.Description
            };
        }

        public static MealTypeListItemDto ToListItemDto(this MealType entity)
        {
            return new MealTypeListItemDto
            {
                Id = entity.Id,
                Name = entity.Name,
                NameEn = entity.NameEn,
                Code = entity.Code,
                GlobalCode = entity.GlobalCode,
                SortOrder = entity.SortOrder,
                Description = entity.Description
            };
        }
    }
}