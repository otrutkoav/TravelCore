using TourCore.Contracts.Geography.Cities;
using TourCore.Domain.Geography.Entities;

namespace TourCore.Application.Cities.Mappings
{
    public static class CityMappingExtensions
    {
        public static CityDto ToDto(this City entity)
        {
            return new CityDto
            {
                Id = entity.Id,
                CountryId = entity.CountryId,
                RegionId = entity.RegionId,
                Name = entity.Name,
                NameEn = entity.NameEn,
                Code = entity.Code,
                SortOrder = entity.SortOrder,
                IsDeparturePoint = entity.IsDeparturePoint,
                TimeZone = entity.TimeZone,
                IataCode = entity.IataCode
            };
        }

        public static CityListItemDto ToListItemDto(this City entity)
        {
            return new CityListItemDto
            {
                Id = entity.Id,
                CountryId = entity.CountryId,
                RegionId = entity.RegionId,
                Name = entity.Name,
                NameEn = entity.NameEn,
                Code = entity.Code,
                SortOrder = entity.SortOrder,
                IsDeparturePoint = entity.IsDeparturePoint,
                IataCode = entity.IataCode
            };
        }
    }
}