namespace TourCore.Application.CharterSeasons.Queries
{
    public class GetCharterSeasonByIdQuery
    {
        public GetCharterSeasonByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }
    }
}