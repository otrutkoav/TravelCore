using TourCore.Domain.Seating.Entities;

namespace TourCore.Application.SeatingCells.DTOs
{
    public class SeatingCellListFilter
    {
        public int? VehiclePlanId { get; set; }
        public SeatType? Type { get; set; }
        public string Number { get; set; }
    }
}