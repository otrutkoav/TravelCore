namespace TourCore.Application.Charters.Queries
{
    public class GetCharterByIdQuery
    {
        public GetCharterByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }
    }
}