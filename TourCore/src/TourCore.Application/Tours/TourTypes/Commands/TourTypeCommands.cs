namespace TourCore.Application.Tours
{
    /// <summary>Команда создания типа тура.</summary>
    public class CreateTourTypeCommand
    {
        /// <summary>Уникальное название типа тура длиной до 128 символов.</summary>
        public string Name { get; set; }

        /// <summary>Идентификатор родительского типа; null означает корневой тип.</summary>
        public int? ParentId { get; set; }
    }

    /// <summary>Команда изменения типа тура.</summary>
    public class UpdateTourTypeCommand
    {
        /// <summary>Идентификатор изменяемого типа из маршрута запроса.</summary>
        public int Id { get; set; }

        /// <summary>Уникальное название типа тура длиной до 128 символов.</summary>
        public string Name { get; set; }

        /// <summary>Идентификатор родительского типа; null означает корневой тип.</summary>
        public int? ParentId { get; set; }
    }

    public class DeleteTourTypeCommand
    {
        public DeleteTourTypeCommand(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }
}
