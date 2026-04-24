using TourCore.Contracts.Geography.Resorts;
using TourCore.Domain.Geography.Entities;

namespace TourCore.Application.Resorts.Mappings
{
    public static class ResortMappingExtensions
    {
        public static ResortDto ToDto(this Resort entity)
        {
            return new ResortDto
            {
                Id = entity.Id,
                CountryId = entity.CountryId,
                Name = entity.Name,
                NameEn = entity.NameEn
            };
        }

        public static ResortListItemDto ToListItemDto(this Resort entity)
        {
            return new ResortListItemDto
            {
                Id = entity.Id,
                CountryId = entity.CountryId,
                Name = entity.Name,
                NameEn = entity.NameEn
            };
        }
    }
}