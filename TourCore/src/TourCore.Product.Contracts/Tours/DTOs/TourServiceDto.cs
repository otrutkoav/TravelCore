using System;

namespace TourCore.Contracts.Tours
{
    /// <summary>Услуга в составе конкретного тура.</summary>
    public class TourServiceDto
    {
        /// <summary>Идентификатор услуги тура.</summary>
        public int Id { get; set; }

        /// <summary>Идентификатор тура.</summary>
        public int TourId { get; set; }

        /// <summary>Идентификатор вида услуги из справочника ServiceTypes.</summary>
        public int ServiceTypeId { get; set; }

        /// <summary>Название услуги в составе тура.</summary>
        public string Name { get; set; }

        /// <summary>Позиция услуги в составе тура, начиная с 1.</summary>
        public int OrderIndex { get; set; }

        /// <summary>Идентификатор родительской услуги; null означает верхний уровень.</summary>
        public int? ParentServiceId { get; set; }

        /// <summary>Скрыта ли услуга в клиентских интерфейсах.</summary>
        public bool IsHidden { get; set; }

        /// <summary>Можно ли удалить услугу из выбранного варианта в корзине.</summary>
        public bool IsRemovable { get; set; }

        /// <summary>Исключена ли услуга из первоначального расчёта до выбора пользователем.</summary>
        public bool IsOptional { get; set; }

        /// <summary>Версия услуги для обнаружения изменений и обновления производных данных.</summary>
        public long Revision { get; set; }

        /// <summary>Дата и время создания в UTC.</summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>Дата и время последнего изменения в UTC.</summary>
        public DateTime? UpdatedAt { get; set; }
    }
}
