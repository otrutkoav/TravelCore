using System;
using System.Collections.Generic;

namespace TourCore.Contracts.Tours
{
    public class TourDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
        public long Revision { get; set; }
        public int CurrencyId { get; set; }
        public int DateMode { get; set; }
        public DateTime? AvailableDateFrom { get; set; }
        public DateTime? AvailableDateTo { get; set; }
        public IReadOnlyCollection<DateTime> AvailableDates { get; set; }
        public IReadOnlyCollection<TourTypeListItemDto> TourTypes { get; set; }
        public string DescriptionUrl { get; set; }
        public string DescriptionHtml { get; set; }
        public string BookingConditionsHtml { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
