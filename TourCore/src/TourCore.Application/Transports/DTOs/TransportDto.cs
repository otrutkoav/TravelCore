using System;

namespace TourCore.Application.Transports.DTOs
{
    public class TransportDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string NameEn { get; set; }

        public short? SeatsCount { get; set; }
        public int ShowOrder { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}