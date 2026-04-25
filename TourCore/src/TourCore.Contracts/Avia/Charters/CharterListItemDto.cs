namespace TourCore.Contracts.Avia.Charters
{
    public class CharterListItemDto
    {
        public int Id { get; set; }

        public int DepartureCityId { get; set; }
        public string DepartureAirportCode { get; set; }

        public int ArrivalCityId { get; set; }
        public string ArrivalAirportCode { get; set; }

        public string AirlineCode { get; set; }
        public string FlightNumber { get; set; }

        public string AircraftCode { get; set; }
        public string AirClassCode { get; set; }

        public short? StopsCount { get; set; }
        public string TimeChangesCode { get; set; }
    }
}