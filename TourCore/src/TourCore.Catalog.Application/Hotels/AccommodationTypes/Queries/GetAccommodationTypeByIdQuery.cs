namespace TourCore.Application.Hotels.AccommodationTypes.Queries
{
    public class GetAccommodationTypeByIdQuery
    {
        public GetAccommodationTypeByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }
    }
}