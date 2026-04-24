using TourCore.Application.Countries.DTOs;

namespace TourCore.Application.Countries.Queries
{
    public class GetCountriesQuery
    {
        public GetCountriesQuery()
        {
            Filter = new CountryListFilter();
        }

        public CountryListFilter Filter { get; set; }
    }
}