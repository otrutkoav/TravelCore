using System.Collections.Generic;

namespace TourCore.Contracts.Tours
{
    /// <summary>Альтернативный вариант выполнения услуги проживания тура.</summary>
    public class TourHotelGroupDto
    {
        /// <summary>Идентификатор группы проживания.</summary>
        public int Id { get; set; }

        /// <summary>Название варианта проживания.</summary>
        public string Name { get; set; }

        /// <summary>Приоритет варианта при подборе.</summary>
        public int Priority { get; set; }

        /// <summary>Позиция группы внутри услуги, начиная с 1.</summary>
        public int OrderIndex { get; set; }

        /// <summary>Последовательные шаблоны проживания данного варианта.</summary>
        public IReadOnlyCollection<TourHotelDto> Hotels { get; set; }
    }
}
