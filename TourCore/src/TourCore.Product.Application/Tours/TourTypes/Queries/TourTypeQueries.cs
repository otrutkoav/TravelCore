using TourCore.Application.Common.Queries;

namespace TourCore.Application.Tours
{
    public class GetTourTypeByIdQuery
    {
        public GetTourTypeByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }

    /// <summary>Параметры получения списка типов туров.</summary>
    public class GetTourTypesQuery : PagedQuery
    {
        /// <summary>Поиск по части названия без учёта регистра.</summary>
        public string Search { get; set; }

        /// <summary>Фильтр по идентификатору непосредственного родителя.</summary>
        public int? ParentId { get; set; }

        /// <summary>Если true, возвращаются только корневые типы.</summary>
        public bool? RootOnly { get; set; }
    }
}
