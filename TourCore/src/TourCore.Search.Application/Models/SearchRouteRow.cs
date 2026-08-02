using System;
using TourCore.Search.Contracts;

namespace TourCore.Search.Application.Models
{
    public sealed class SearchRouteRow
    {
        public SearchTransportKind Kind { get; set; }
        public int RouteId { get; set; }
        public int ScheduleId { get; set; }
        public string Name { get; set; }
        public int DepartureCityId { get; set; }
        public string DepartureCityName { get; set; }
        public int ArrivalCityId { get; set; }
        public string ArrivalCityName { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string DaysOfWeek { get; set; }
        public DateTime? TimeFrom { get; set; }
        public DateTime? TimeTo { get; set; }
        public short? DaysOnRoad { get; set; }
        public string CarrierCode { get; set; }
        public string Number { get; set; }
    }
}
