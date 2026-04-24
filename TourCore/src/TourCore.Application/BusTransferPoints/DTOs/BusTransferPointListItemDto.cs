using System;

namespace TourCore.Application.BusTransferPoints.DTOs
{
    public class BusTransferPointListItemDto
    {
        public int Id { get; set; }

        public int BusTransferId { get; set; }

        public int CountryFromId { get; set; }
        public int CityFromId { get; set; }

        public int CountryToId { get; set; }
        public int CityToId { get; set; }

        public DateTime? TimeFrom { get; set; }
        public DateTime? TimeTo { get; set; }

        public short? DayFrom { get; set; }
        public short? DayTo { get; set; }
    }
}