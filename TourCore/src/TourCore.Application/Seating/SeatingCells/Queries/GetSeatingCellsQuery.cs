using TourCore.Application.Common.Queries;
using TourCore.Application.Seating.SeatingCells.DTOs;

namespace TourCore.Application.Seating.SeatingCells.Queries
{
    /// <summary>
    /// Запрос списка ячеек схем размещения.
    /// </summary>
    public class GetSeatingCellsQuery : PagedQuery
    {
        public GetSeatingCellsQuery()
        {
            Filter = new SeatingCellListFilter();
        }

        /// <summary>
        /// Фильтр списка ячеек схем размещения.
        /// </summary>
        public SeatingCellListFilter Filter { get; set; }
    }
}