namespace TourCore.Application.Avia.CharterSeasons.Queries
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