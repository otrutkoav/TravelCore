namespace TourCore.Contracts.Tours
{
    /// <summary>Краткие данные типа тура в списке.</summary>
    public class TourTypeListItemDto
    {
        /// <summary>Идентификатор типа тура.</summary>
        public int Id { get; set; }

        /// <summary>Название типа тура.</summary>
        public string Name { get; set; }

        /// <summary>Идентификатор родительского типа; null означает корневой тип.</summary>
        public int? ParentId { get; set; }
    }
}
