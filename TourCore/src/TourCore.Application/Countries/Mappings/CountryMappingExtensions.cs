using TourCore.Contracts.Geography.Countries;
using TourCore.Domain.Geography.Entities;

namespace TourCore.Application.Countries.Mappings
{
    public static class CountryMappingExtensions
    {
        public static CountryDto ToDto(this Country entity)
        {
            return new CountryDto
            {
                Id = entity.Id,
                Name = entity.Name,
                NameEn = entity.NameEn,
                Code = entity.Code,
                IsoCode2 = entity.IsoCode2,
                IsoCode3 = entity.IsoCode3,
                DigitalCode = entity.DigitalCode,
                CitizenshipName = entity.CitizenshipName,
                CitizenshipNameEn = entity.CitizenshipNameEn,
                SortOrder = entity.SortOrder
            };
        }

        public static CountryListItemDto ToListItemDto(this Country entity)
        {
            return new CountryListItemDto
            {
                Id = entity.Id,
                Name = entity.Name,
                NameEn = entity.NameEn,
                Code = entity.Code,
                IsoCode2 = entity.IsoCode2,
                IsoCode3 = entity.IsoCode3,
                SortOrder = entity.SortOrder
            };
        }
    }
}