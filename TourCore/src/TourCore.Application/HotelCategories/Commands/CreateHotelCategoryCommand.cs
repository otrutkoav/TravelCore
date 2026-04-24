namespace TourCore.Application.HotelCategories.Commands
{
    public class CreateHotelCategoryCommand
    {
        public string Name { get; set; }
        public string NameEn { get; set; }
        public int? PrintOrder { get; set; }
        public string GlobalCode { get; set; }
        public string Description { get; set; }
    }
}