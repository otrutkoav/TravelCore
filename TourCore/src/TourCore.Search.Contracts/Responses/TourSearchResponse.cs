namespace TourCore.Search.Contracts
{
    /// <summary>Результат поиска и статусы ещё не подключённых расчётов.</summary>
    public sealed class TourSearchResponse
    {
        public TourSearchResponse(SearchPagedResult<TourCandidate> results)
        {
            Results = results;
            PriceStatus = "NotCalculated";
            AvailabilityStatus = "NotChecked";
        }
        /// <summary>Страница кандидатов тура.</summary>
        public SearchPagedResult<TourCandidate> Results { get; }
        /// <summary>Статус расчёта цены. В первой версии всегда NotCalculated.</summary>
        public string PriceStatus { get; }
        /// <summary>Статус проверки квот и доступности. В первой версии всегда NotChecked.</summary>
        public string AvailabilityStatus { get; }
    }
}
