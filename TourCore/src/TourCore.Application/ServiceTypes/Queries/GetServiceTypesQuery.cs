using TourCore.Application.ServiceTypes.DTOs;

namespace TourCore.Application.ServiceTypes.Queries
{
    public class GetServiceTypesQuery
    {
        public GetServiceTypesQuery()
        {
            Filter = new ServiceTypeListFilter();
        }

        public ServiceTypeListFilter Filter { get; set; }
    }
}