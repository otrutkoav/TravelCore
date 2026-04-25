namespace TourCore.Contracts.Hotels.HotelCategories
{
    public class HotelCategoryListItemDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string NameEn { get; set; }

        public int? PrintOrder { get; set; }

        public string GlobalCode { get; set; }

        public string Description { get; set; }
    }
}