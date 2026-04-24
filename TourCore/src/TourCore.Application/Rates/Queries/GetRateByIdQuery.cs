namespace TourCore.Application.Rates.Queries
{
    public class GetRateByIdQuery
    {
        public GetRateByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }
    }
}