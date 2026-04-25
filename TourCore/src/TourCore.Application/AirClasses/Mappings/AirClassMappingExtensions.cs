using TourCore.Contracts.Avia.AirClasses;
using TourCore.Domain.Avia.Entities;

namespace TourCore.Application.AirClasses.Mappings
{
    public static class AirClassMappingExtensions
    {
        public static AirClassDto ToDto(this AirClass entity)
        {
            return new AirClassDto
            {
                Id = entity.Id,
                Code = entity.Code,
                Name = entity.Name,
                NameEn = entity.NameEn,
                Group = entity.Group,
                SortOrder = entity.SortOrder
            };
        }

        public static AirClassListItemDto ToListItemDto(this AirClass entity)
        {
            return new AirClassListItemDto
            {
                Id = entity.Id,
                Code = entity.Code,
                Name = entity.Name,
                NameEn = entity.NameEn,
                Group = entity.Group,
                SortOrder = entity.SortOrder
            };
        }
    }
}