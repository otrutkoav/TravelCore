using TourCore.Application.Geography.Countries.DTOs;

namespace TourCore.Application.Geography.Countries.Queries
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