using TourCore.Contracts.Hotels.Hotels;
using TourCore.Domain.Hotels.Entities;

namespace TourCore.Application.Hotels.Mappings
{
    public static class HotelMappingExtensions
    {
        public static HotelDto ToDto(this Hotel entity)
        {
            return new HotelDto
            {
                Id = entity.Id,
                CountryId = entity.CountryId,
                CityId = entity.CityId,
                ResortId = entity.ResortId,
                CategoryId = entity.CategoryId,
                Name = entity.Name,
                NameEn = entity.NameEn,
                Stars = entity.Stars,
                Code = entity.Code,
                Address = entity.Address,
                Phone = entity.Phone,
                Fax = entity.Fax,
                Email = entity.Email,
                Website = entity.Website,
                Latitude = entity.Latitude,
                Longitude = entity.Longitude,
                IsCruise = entity.IsCruise,
                SortOrder = entity.SortOrder,
                Rank = entity.Rank
            };
        }

        public static HotelListItemDto ToListItemDto(this Hotel entity)
        {
            return new HotelListItemDto
            {
                Id = entity.Id,
                CountryId = entity.CountryId,
                CityId = entity.CityId,
                ResortId = entity.ResortId,
                CategoryId = entity.CategoryId,
                Name = entity.Name,
                NameEn = entity.NameEn,
                Stars = entity.Stars,
                Code = entity.Code,
                Address = entity.Address,
                Phone = entity.Phone,
                Fax = entity.Fax,
                Email = entity.Email,
                Website = entity.Website,
                Latitude = entity.Latitude,
                Longitude = entity.Longitude,
                IsCruise = entity.IsCruise,
                SortOrder = entity.SortOrder,
                Rank = entity.Rank
            };
        }
    }
}