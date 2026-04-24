namespace TourCore.Application.VehiclePlans.DTOs
{
    public class VehiclePlanListItemDto
    {
        public int Id { get; set; }

        public int TransportId { get; set; }

        public int RowsCount { get; set; }
        public int ColumnsCount { get; set; }
        public int AreaNumber { get; set; }

        public string Name { get; set; }

        public bool IsAircraft { get; set; }
    }
}