namespace TourCore.Application.Railway.RailwayTransfers.Queries
{
    public class GetRailwayTransferByIdQuery
    {
        public GetRailwayTransferByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }
}