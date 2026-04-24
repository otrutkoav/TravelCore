namespace TourCore.Application.RoomTypes.Queries
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