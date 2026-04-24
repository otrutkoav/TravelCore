using TourCore.Application.Cities.DTOs;

namespace TourCore.Application.Cities.Queries
{
    public class GetCitiesQuery
    {
        public GetCitiesQuery()
        {
            Filter = new CityListFilter();
        }

        public CityListFilter Filter { get; set; }
    }
}