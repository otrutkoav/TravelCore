using TourCore.Application.Finance.Rates.DTOs;

namespace TourCore.Application.Finance.Rates.Queries
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