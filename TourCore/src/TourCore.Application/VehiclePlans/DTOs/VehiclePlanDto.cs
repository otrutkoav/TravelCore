using System;

namespace TourCore.Application.VehiclePlans.DTOs
{
    public class VehiclePlanDto
    {
        public int Id { get; set; }

        public int TransportId { get; set; }

        public int RowsCount { get; set; }
        public int ColumnsCount { get; set; }
        public int AreaNumber { get; set; }

        public string Name { get; set; }

        public bool PlanOrientation { get; set; }
        public bool IsAircraft { get; set; }

        public string Dates { get; set; }
        public string Comment { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}