using TourCore.Contracts.Seating.Enum;

namespace TourCore.Application.Seating.SeatingCells.DTOs
{
    public class SeatingCellListFilter
    {
        /// <summary>
        /// Фильтр по идентификатору схемы транспорта.
        /// </summary>
        public int? VehiclePlanId { get; set; }

        /// <summary>
        /// Фильтр по типу ячейки схемы размещения.
        /// Допустимые значения: 0 — не определено, 1 — место, 4 — блок, 5 — занятое место.
        /// </summary>
        public SeatType? Type { get; set; }

        /// <summary>
        /// Поиск по отображаемому номеру ячейки.
        /// </summary>
        public string Number { get; set; }
    }
}