using System.Collections.Generic;

namespace TourCore.Contracts.Tours
{
    /// <summary>Альтернативный вариант выполнения авиационной услуги тура.</summary>
    public class TourFlightGroupDto
    {
        /// <summary>Идентификатор группы перелётов.</summary>
        public int Id { get; set; }

        /// <summary>Название варианта перелёта.</summary>
        public string Name { get; set; }

        /// <summary>Приоритет варианта при подборе.</summary>
        public int Priority { get; set; }

        /// <summary>Позиция группы внутри авиационной услуги, начиная с 1.</summary>
        public int OrderIndex { get; set; }

        /// <summary>Последовательные перелёты данного варианта.</summary>
        public IReadOnlyCollection<TourFlightDto> Flights { get; set; }
    }
}
