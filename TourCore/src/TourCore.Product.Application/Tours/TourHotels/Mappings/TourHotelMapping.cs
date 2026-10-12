using System.Linq;
using TourCore.Contracts.Tours;
using TourCore.Domain.Tours;

namespace TourCore.Application.Tours
{
    internal static class TourHotelMapping
    {
        public static TourHotelDto ToDto(this TourHotel hotel)
        {
            return new TourHotelDto
            {
                Id = hotel.Id,
                OrderIndex = hotel.OrderIndex,
                CountryId = hotel.CountryId,
                CityIds = hotel.Cities.Select(x => x.CityId).OrderBy(x => x).ToArray(),
                DurationsInNights = hotel.Durations.Select(x => x.Nights).OrderBy(x => x).ToArray(),
                HotelIds = hotel.AllowedHotels.Select(x => x.HotelId).OrderBy(x => x).ToArray(),
            };
        }

        public static TourHotelGroupDto ToDto(this TourHotelGroup group)
        {
            return new TourHotelGroupDto
            {
                Id = group.Id,
                Name = group.Name,
                Priority = group.Priority,
                OrderIndex = group.OrderIndex,
                Hotels = group.Hotels.OrderBy(x => x.OrderIndex).Select(ToDto).ToArray(),
            };
        }
    }
}
