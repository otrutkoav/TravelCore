namespace TourCore.Application.Hotels.RoomTypes.Queries
{
    public class GetRoomTypeByIdQuery
    {
        public GetRoomTypeByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }
    }
}