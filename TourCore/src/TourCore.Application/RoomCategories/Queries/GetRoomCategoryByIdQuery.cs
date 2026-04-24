namespace TourCore.Application.RoomCategories.Queries
{
    public class GetRoomCategoryByIdQuery
    {
        public GetRoomCategoryByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }
    }
}