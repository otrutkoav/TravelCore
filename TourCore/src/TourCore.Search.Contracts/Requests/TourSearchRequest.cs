using System;
using System.Collections.Generic;

namespace TourCore.Search.Contracts
{
    /// <summary>Основные параметры поиска кандидатов тура.</summary>
    public sealed class TourSearchRequest
    {
        public TourSearchRequest()
        {
            Dates = new List<DateTime>();
            DurationsInNights = new List<int>();
            Adults = 2;
            Page = 1;
            PageSize = 20;
        }

        /// <summary>Идентификатор города отправления. Значение -1 означает поиск без транспорта.</summary>
        public int DepartureCityId { get; set; }
        /// <summary>Обязательный идентификатор страны назначения.</summary>
        public int DestinationCountryId { get; set; }
        /// <summary>Необязательный идентификатор города назначения. Null означает все города выбранной страны.</summary>
        public int? DestinationCityId { get; set; }
        /// <summary>Даты начала поездки без компонента времени. Допускается от 1 до 10 дат.</summary>
        public IList<DateTime> Dates { get; set; }
        /// <summary>Продолжительности поездки в ночах. Допускается от 1 до 10 значений, каждое от 1 до 60.</summary>
        public IList<int> DurationsInNights { get; set; }
        /// <summary>Количество взрослых туристов от 1 до 20.</summary>
        public int Adults { get; set; }
        /// <summary>Номер страницы, начиная с 1. По умолчанию 1.</summary>
        public int Page { get; set; }
        /// <summary>Размер страницы от 1 до 100. По умолчанию 20.</summary>
        public int PageSize { get; set; }
    }
}
