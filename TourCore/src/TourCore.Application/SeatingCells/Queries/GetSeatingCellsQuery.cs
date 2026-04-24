using TourCore.Application.SeatingCells.DTOs;

namespace TourCore.Application.SeatingCells.Queries
{
    public class GetSeatingCellsQuery
    {
        public GetSeatingCellsQuery()
        {
            Filter = new SeatingCellListFilter();
        }

        public SeatingCellListFilter Filter { get; set; }
    }
}