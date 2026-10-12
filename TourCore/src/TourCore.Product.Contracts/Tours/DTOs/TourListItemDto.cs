using System;

namespace TourCore.Contracts.Tours
{
    public class TourListItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
        public long Revision { get; set; }
        public int CurrencyId { get; set; }
        public int DateMode { get; set; }
        public DateTime? AvailableDateFrom { get; set; }
        public DateTime? AvailableDateTo { get; set; }
    }
}
