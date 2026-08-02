using System.Collections.Generic;

namespace TourCore.Search.Application.Models
{
    public sealed class SearchDataSet
    {
        public SearchDataSet()
        {
            Hotels = new List<SearchHotelRow>();
            Routes = new List<SearchRouteRow>();
            Transfers = new List<SearchTransferRow>();
        }
        public IReadOnlyCollection<SearchHotelRow> Hotels { get; set; }
        public IReadOnlyCollection<SearchRouteRow> Routes { get; set; }
        public IReadOnlyCollection<SearchTransferRow> Transfers { get; set; }
    }
}
