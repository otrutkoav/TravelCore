using TourCore.Application.Airlines.DTOs;

namespace TourCore.Application.Airlines.Queries
{
    public class GetAirlinesQuery
    {
        public GetAirlinesQuery()
        {
            Filter = new AirlineListFilter();
        }

        public AirlineListFilter Filter { get; set; }
    }
}