using System;

namespace TourCore.Contracts.Transfers.Transfers
{
    public class TransferDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string NameEn { get; set; }

        public DateTime? TimeFrom { get; set; }
        public DateTime? TimeTo { get; set; }

        public string DurationText { get; set; }

        public string PlaceFrom { get; set; }
        public string PlaceTo { get; set; }

        public bool IsMain { get; set; }

        public int? CityId { get; set; }
        public int? DirectionId { get; set; }

        public string Url { get; set; }

        public int ShowOrder { get; set; }

        public bool AutoApplyFrom { get; set; }
        public bool AutoApplyTo { get; set; }
    }
}