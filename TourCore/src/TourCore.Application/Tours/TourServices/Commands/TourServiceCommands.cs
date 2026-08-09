using System.Collections.Generic;

namespace TourCore.Application.Tours
{
    /// <summary>Команда добавления услуги в состав тура.</summary>
    public class CreateTourServiceCommand
    {
        /// <summary>Идентификатор тура из маршрута запроса.</summary>
        public int TourId { get; set; }

        /// <summary>Идентификатор вида услуги из справочника ServiceTypes.</summary>
        public int ServiceTypeId { get; set; }

        /// <summary>Название услуги в составе тура длиной до 256 символов.</summary>
        public string Name { get; set; }

        /// <summary>Идентификатор родительской услуги того же тура; null означает верхний уровень.</summary>
        public int? ParentServiceId { get; set; }

        /// <summary>Скрывать ли услугу в клиентских интерфейсах.</summary>
        public bool IsHidden { get; set; }

        /// <summary>Можно ли удалить услугу из выбранного варианта в корзине.</summary>
        public bool IsRemovable { get; set; }

        /// <summary>Исключать ли услугу из первоначального расчёта до выбора пользователем.</summary>
        public bool IsOptional { get; set; }
    }

    /// <summary>Команда изменения услуги в составе тура.</summary>
    public class UpdateTourServiceCommand : CreateTourServiceCommand
    {
        /// <summary>Идентификатор изменяемой услуги из маршрута запроса.</summary>
        public int Id { get; set; }
    }

    public class DeleteTourServiceCommand
    {
        public DeleteTourServiceCommand(int tourId, int id)
        {
            TourId = tourId;
            Id = id;
        }

        public int TourId { get; }
        public int Id { get; }
    }

    /// <summary>Команда изменения порядка всех услуг тура.</summary>
    public class ReorderTourServicesCommand
    {
        /// <summary>Идентификатор тура из маршрута запроса.</summary>
        public int TourId { get; set; }

        /// <summary>Все идентификаторы услуг тура в требуемом порядке.</summary>
        public ICollection<int> ServiceIds { get; set; } = new List<int>();
    }
}
