using System;

namespace TourCore.Contracts.Tours
{
    /// <summary>Полные данные типа тура.</summary>
    public class TourTypeDto
    {
        /// <summary>Идентификатор типа тура.</summary>
        public int Id { get; set; }

        /// <summary>Название типа тура.</summary>
        public string Name { get; set; }

        /// <summary>Идентификатор родительского типа; null означает корневой тип.</summary>
        public int? ParentId { get; set; }

        /// <summary>Дата и время создания в UTC.</summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>Дата и время последнего изменения в UTC.</summary>
        public DateTime? UpdatedAt { get; set; }
    }
}
