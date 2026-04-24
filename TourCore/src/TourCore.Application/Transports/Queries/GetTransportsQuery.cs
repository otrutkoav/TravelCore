using TourCore.Application.Transports.DTOs;

namespace TourCore.Application.Transports.Queries
{
    public class GetTransportsQuery
    {
        public GetTransportsQuery()
        {
            Filter = new TransportListFilter();
        }

        public TransportListFilter Filter { get; set; }
    }
}