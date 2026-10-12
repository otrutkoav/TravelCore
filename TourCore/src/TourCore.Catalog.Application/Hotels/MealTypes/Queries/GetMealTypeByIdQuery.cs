namespace TourCore.Application.Hotels.MealTypes.Queries
{
    public class GetMealTypeByIdQuery
    {
        public GetMealTypeByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }
    }
}