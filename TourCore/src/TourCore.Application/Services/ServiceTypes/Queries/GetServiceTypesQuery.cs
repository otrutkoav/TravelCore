using TourCore.Application.Services.ServiceTypes.DTOs;

namespace TourCore.Application.Services.ServiceTypes.Queries
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