using TourCore.Contracts.Avia.Aircrafts;
using TourCore.Domain.Avia.Entities;

namespace TourCore.Application.Aircrafts.Mappings
{
    public static class AircraftMappingExtensions
    {
        public static AircraftDto ToDto(this Aircraft entity)
        {
            return new AircraftDto
            {
                Id = entity.Id,
                Code = entity.Code,
                Name = entity.Name,
                NameEn = entity.NameEn
            };
        }

        public static AircraftListItemDto ToListItemDto(this Aircraft entity)
        {
            return new AircraftListItemDto
            {
                Id = entity.Id,
                Code = entity.Code,
                Name = entity.Name
            };
        }
    }
}