using TourCore.Domain.Seating.Entities;

namespace TourCore.Application.SeatingCells.DTOs
{
    public class SeatingCellListItemDto
    {
        public int Id { get; set; }

        public int VehiclePlanId { get; set; }

        public string Number { get; set; }
        public SeatType? Type { get; set; }
        public int Index { get; set; }
    }
}