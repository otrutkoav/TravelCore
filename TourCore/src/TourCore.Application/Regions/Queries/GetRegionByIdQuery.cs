namespace TourCore.Application.Regions.Queries
{
    public class GetRegionByIdQuery
    {
        public GetRegionByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }
    }
}