using TourCore.Contracts.Avia.Airlines;
using TourCore.Domain.Avia.Entities;

namespace TourCore.Application.Airlines.Mappings
{
    public static class AirlineMappingExtensions
    {
        public static AirlineDto ToDto(this Airline entity)
        {
            return new AirlineDto
            {
                Id = entity.Id,
                Code = entity.Code,
                Name = entity.Name,
                NameEn = entity.NameEn,
                IcaoCode = entity.IcaoCode
            };
        }

        public static AirlineListItemDto ToListItemDto(this Airline entity)
        {
            return new AirlineListItemDto
            {
                Id = entity.Id,
                Code = entity.Code,
                Name = entity.Name,
                IcaoCode = entity.IcaoCode
            };
        }
    }
}