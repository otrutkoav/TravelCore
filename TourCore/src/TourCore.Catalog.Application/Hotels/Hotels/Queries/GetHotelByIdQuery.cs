namespace TourCore.Application.Hotels.Hotels.Queries
{
    public class GetHotelByIdQuery
    {
        public GetHotelByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }
    }
}