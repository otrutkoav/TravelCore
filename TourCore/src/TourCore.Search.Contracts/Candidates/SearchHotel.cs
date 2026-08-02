namespace TourCore.Search.Contracts
{
    /// <summary>Краткие данные отеля в поисковой выдаче.</summary>
    public sealed class SearchHotel
    {
        /// <summary>Идентификатор отеля.</summary>
        public int Id { get; set; }
        /// <summary>Код отеля.</summary>
        public string Code { get; set; }
        /// <summary>Название отеля.</summary>
        public string Name { get; set; }
        /// <summary>Текстовое обозначение звёздности.</summary>
        public string Stars { get; set; }
        /// <summary>Идентификатор категории отеля.</summary>
        public int? CategoryId { get; set; }
        /// <summary>Название категории отеля.</summary>
        public string CategoryName { get; set; }
        /// <summary>Идентификатор курорта.</summary>
        public int? ResortId { get; set; }
        /// <summary>Название курорта.</summary>
        public string ResortName { get; set; }
        /// <summary>Адрес отеля.</summary>
        public string Address { get; set; }
        /// <summary>Внутренний ранг отеля.</summary>
        public int? Rank { get; set; }
    }
}
