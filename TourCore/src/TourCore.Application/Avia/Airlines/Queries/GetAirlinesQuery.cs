using TourCore.Application.Avia.Airlines.DTOs;

namespace TourCore.Application.Avia.Airlines.Queries
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