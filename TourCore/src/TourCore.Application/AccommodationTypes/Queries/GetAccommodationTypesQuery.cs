using TourCore.Application.AccommodationTypes.DTOs;

namespace TourCore.Application.AccommodationTypes.Queries
{
    public class GetAccommodationTypesQuery
    {
        public GetAccommodationTypesQuery()
        {
            Filter = new AccommodationTypeListFilter();
        }

        public AccommodationTypeListFilter Filter { get; set; }
    }
}