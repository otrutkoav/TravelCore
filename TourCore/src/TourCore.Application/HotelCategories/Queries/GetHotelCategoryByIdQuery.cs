namespace TourCore.Application.HotelCategories.Queries
{
    public class GetHotelCategoryByIdQuery
    {
        public GetHotelCategoryByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }
    }
}