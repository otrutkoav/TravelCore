using TourCore.Contracts.Avia.Airports;
using TourCore.Domain.Avia.Entities;

namespace TourCore.Application.Airports.Mappings
{
    public static class AirportMappingExtensions
    {
        public static AirportDto ToDto(this Airport entity)
        {
            return new AirportDto
            {
                Id = entity.Id,
                CityId = entity.CityId,
                Code = entity.Code,
                Name = entity.Name,
                NameEn = entity.NameEn,
                IcaoCode = entity.IcaoCode,
                LetterCode = entity.LetterCode
            };
        }

        public static AirportListItemDto ToListItemDto(this Airport entity)
        {
            return new AirportListItemDto
            {
                Id = entity.Id,
                CityId = entity.CityId,
                Code = entity.Code,
                Name = entity.Name,
                NameEn = entity.NameEn,
                IcaoCode = entity.IcaoCode,
                LetterCode = entity.LetterCode
            };
        }
    }
}