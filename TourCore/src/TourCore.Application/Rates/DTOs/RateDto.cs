using System;

namespace TourCore.Application.Rates.DTOs
{
    public class RateDto
    {
        public int Id { get; set; }

        public string Code { get; set; }
        public string Name { get; set; }
        public string IsoCode { get; set; }

        public bool IsMain { get; set; }
        public bool IsNational { get; set; }
        public bool ShowInSearch { get; set; }

        public byte[] Symbol { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}