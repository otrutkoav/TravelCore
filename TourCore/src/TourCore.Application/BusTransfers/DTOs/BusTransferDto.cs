using System;

namespace TourCore.Application.BusTransfers.DTOs
{
    public class BusTransferDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int CountryFromId { get; set; }
        public int CityFromId { get; set; }

        public int CountryToId { get; set; }
        public int CityToId { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}