using System;
using System.Collections.Generic;
using System.Text;

namespace TourCore.Contracts.Seating.Enum
{
    /// <summary>
    /// Тип ячейки схемы размещения.
    /// </summary>
    public enum SeatType : short
    {
        /// <summary>
        /// Не определено.
        /// </summary>
        None = 0,

        /// <summary>
        /// Место для размещения пассажира.
        /// </summary>
        Seat = 1,

        /// <summary>
        /// Блок / недоступная область схемы.
        /// </summary>
        Block = 4,

        /// <summary>
        /// Занятое место.
        /// </summary>
        Busy = 5
    }
}
