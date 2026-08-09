using System.Collections.Generic;

namespace TourCore.Contracts.Tours
{
    /// <summary>Шаблон участка проживания внутри варианта услуги тура.</summary>
    public class TourHotelDto
    {
        /// <summary>Идентификатор шаблона проживания.</summary>
        public int Id { get; set; }

        /// <summary>Позиция шаблона внутри группы, начиная с 1.</summary>
        public int OrderIndex { get; set; }

        /// <summary>Идентификатор страны проживания из справочника.</summary>
        public int CountryId { get; set; }

        /// <summary>Допустимые идентификаторы городов проживания из справочника.</summary>
        public IReadOnlyCollection<int> CityIds { get; set; }

        /// <summary>Допустимые продолжительности проживания в ночах.</summary>
        public IReadOnlyCollection<int> DurationsInNights { get; set; }

        /// <summary>Допустимые идентификаторы отелей выбранного города.</summary>
        public IReadOnlyCollection<int> HotelIds { get; set; }
    }
}
