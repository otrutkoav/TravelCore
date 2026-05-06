using TourCore.Contracts.Seating.Enum;
using TourCore.Contracts.Seating.SeatingCells;

namespace TourCore.Contracts.Seating.SeatingCells
{
    public class SeatingCellListItemDto
    {
        public int Id { get; set; }

        public int VehiclePlanId { get; set; }

        public string Number { get; set; }

        public SeatType? Type { get; set; }

        public short? SeatsCount { get; set; }

        public int Index { get; set; }

        public string Border { get; set; }
    }
}