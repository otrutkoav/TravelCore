using TourCore.Contracts.Avia.Charters;
using TourCore.Domain.Avia.Entities;

namespace TourCore.Application.Charters.Mappings
{
    public static class CharterMappingExtensions
    {
        public static CharterDto ToDto(this Charter entity)
        {
            return new CharterDto
            {
                Id = entity.Id,
                DepartureCityId = entity.DepartureCityId,
                DepartureAirportCode = entity.DepartureAirportCode,
                ArrivalCityId = entity.ArrivalCityId,
                ArrivalAirportCode = entity.ArrivalAirportCode,
                AirlineCode = entity.AirlineCode,
                FlightNumber = entity.FlightNumber,
                AircraftCode = entity.AircraftCode,
                AirClassCode = entity.AirClassCode,
                StopsCount = entity.StopsCount,
                TimeChangesCode = entity.TimeChangesCode
            };
        }

        public static CharterListItemDto ToListItemDto(this Charter entity)
        {
            return new CharterListItemDto
            {
                Id = entity.Id,
                DepartureCityId = entity.DepartureCityId,
                DepartureAirportCode = entity.DepartureAirportCode,
                ArrivalCityId = entity.ArrivalCityId,
                ArrivalAirportCode = entity.ArrivalAirportCode,
                AirlineCode = entity.AirlineCode,
                FlightNumber = entity.FlightNumber,
                AircraftCode = entity.AircraftCode,
                AirClassCode = entity.AirClassCode,
                StopsCount = entity.StopsCount,
                TimeChangesCode = entity.TimeChangesCode
            };
        }
    }
}