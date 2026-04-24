namespace TourCore.Application.HotelRoomCombinations.Queries
{
    public class GetHotelRoomCombinationByIdQuery
    {
        public GetHotelRoomCombinationByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }
    }
}