using TourCore.Application.Hotels.AccommodationTypes.DTOs;

namespace TourCore.Application.Hotels.AccommodationTypes.Queries
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