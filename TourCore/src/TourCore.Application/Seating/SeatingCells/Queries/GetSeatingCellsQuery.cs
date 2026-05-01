using TourCore.Application.Seating.SeatingCells.DTOs;

namespace TourCore.Application.Seating.SeatingCells.Queries
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