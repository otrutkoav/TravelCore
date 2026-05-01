using TourCore.Application.Geography.Cities.DTOs;

namespace TourCore.Application.Geography.Cities.Queries
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