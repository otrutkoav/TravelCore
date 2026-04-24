using TourCore.Domain.Seating.Entities;

namespace TourCore.Application.SeatingCells.Commands
{
    public class CreateSeatingCellCommand
    {
        public int VehiclePlanId { get; set; }

        public string Number { get; set; }
        public SeatType? Type { get; set; }
        public short? SeatsCount { get; set; }

        public int Index { get; set; }
        public string Border { get; set; }
    }
}