using System;
using TourCore.Domain.Seating.Entities;

namespace TourCore.Application.SeatingCells.DTOs
{
    public class SeatingCellDto
    {
        public int Id { get; set; }

        public int VehiclePlanId { get; set; }

        public string Number { get; set; }
        public SeatType? Type { get; set; }
        public short? SeatsCount { get; set; }

        public int Index { get; set; }
        public string Border { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}