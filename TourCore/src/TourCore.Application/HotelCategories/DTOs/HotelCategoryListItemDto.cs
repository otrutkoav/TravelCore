namespace TourCore.Application.HotelCategories.DTOs
{
    public class HotelCategoryListItemDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string GlobalCode { get; set; }
        public int? PrintOrder { get; set; }
    }
}