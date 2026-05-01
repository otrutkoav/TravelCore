using TourCore.Application.Transportation.Transports.DTOs;

namespace TourCore.Application.Transportation.Transports.Queries
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