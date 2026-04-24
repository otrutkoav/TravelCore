using System;

namespace TourCore.Application.ServiceTypes.DTOs
{
    public class ServiceTypeDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string NameEn { get; set; }
        public string Code { get; set; }

        public short? Category { get; set; }
        public int? ControlMode { get; set; }

        public bool IsCity { get; set; }
        public bool HasPrimaryParameter { get; set; }
        public bool HasSecondaryParameter { get; set; }
        public bool RoundGrossAmount { get; set; }

        public bool IsDuration { get; set; }
        public bool UseManualInput { get; set; }
        public bool IsQuoted { get; set; }
        public bool IsIndividual { get; set; }

        public decimal? SmallAmountPercent { get; set; }
        public short? SmallAmountThreshold { get; set; }
        public bool UseSmallAmountAndRule { get; set; }

        public bool IsRoute { get; set; }
        public bool IsPartnerBasedOn { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}