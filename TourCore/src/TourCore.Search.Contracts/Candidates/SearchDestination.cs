namespace TourCore.Search.Contracts
{
    /// <summary>Направление назначения.</summary>
    public sealed class SearchDestination
    {
        /// <summary>Идентификатор страны.</summary>
        public int CountryId { get; set; }
        /// <summary>Название страны.</summary>
        public string CountryName { get; set; }
        /// <summary>Идентификатор города.</summary>
        public int CityId { get; set; }
        /// <summary>Название города.</summary>
        public string CityName { get; set; }
    }
}
