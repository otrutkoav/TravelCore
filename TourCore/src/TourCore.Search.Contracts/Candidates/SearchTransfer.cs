namespace TourCore.Search.Contracts
{
    /// <summary>Трансфер, доступный в городе назначения.</summary>
    public sealed class SearchTransfer
    {
        /// <summary>Идентификатор трансфера.</summary>
        public int Id { get; set; }
        /// <summary>Название трансфера.</summary>
        public string Name { get; set; }
        /// <summary>Текстовая продолжительность трансфера.</summary>
        public string DurationText { get; set; }
        /// <summary>Место отправления.</summary>
        public string PlaceFrom { get; set; }
        /// <summary>Место прибытия.</summary>
        public string PlaceTo { get; set; }
    }
}
