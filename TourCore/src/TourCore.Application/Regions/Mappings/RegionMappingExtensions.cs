using TourCore.Contracts.Geography.Regions;
using TourCore.Domain.Geography.Entities;

namespace TourCore.Application.Regions.Mappings
{
    public static class RegionMappingExtensions
    {
        public static RegionDto ToDto(this Region entity)
        {
            return new RegionDto
            {
                Id = entity.Id,
                CountryId = entity.CountryId,
                Name = entity.Name,
                NameEn = entity.NameEn,
                Code = entity.Code,
                SortOrder = entity.SortOrder
            };
        }

        public static RegionListItemDto ToListItemDto(this Region entity)
        {
            return new RegionListItemDto
            {
                Id = entity.Id,
                CountryId = entity.CountryId,
                Name = entity.Name,
                NameEn = entity.NameEn,
                Code = entity.Code,
                SortOrder = entity.SortOrder
            };
        }
    }
}