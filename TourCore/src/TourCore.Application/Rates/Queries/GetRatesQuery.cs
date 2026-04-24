using TourCore.Application.Rates.DTOs;

namespace TourCore.Application.Rates.Queries
{
    public class GetRatesQuery
    {
        public GetRatesQuery()
        {
            Filter = new RateListFilter();
        }

        public RateListFilter Filter { get; set; }
    }
}