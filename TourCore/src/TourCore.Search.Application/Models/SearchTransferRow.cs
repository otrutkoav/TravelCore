namespace TourCore.Search.Application.Models
{
    public sealed class SearchTransferRow
    {
        public int Id { get; set; }
        public int CityId { get; set; }
        public string Name { get; set; }
        public string DurationText { get; set; }
        public string PlaceFrom { get; set; }
        public string PlaceTo { get; set; }
        public int ShowOrder { get; set; }
    }
}
