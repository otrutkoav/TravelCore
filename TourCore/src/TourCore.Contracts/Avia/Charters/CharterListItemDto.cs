namespace TourCore.Contracts.Avia.Charters
{
    public class CharterListItemDto
    {
        public int Id { get; set; }

        public int DepartureCityId { get; set; }

        public int ArrivalCityId { get; set; }

        public string FlightNumber { get; set; }

        public string AirlineCode { get; set; }
    }
}