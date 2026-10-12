namespace TourCore.Application.Services.ServiceTypes.Queries
{
    public class GetServiceTypeByIdQuery
    {
        public GetServiceTypeByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }
    }
}