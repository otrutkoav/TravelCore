namespace TourCore.Application.MealTypes.DTOs
{
    public class MealTypeListItemDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string NameEn { get; set; }
        public string Code { get; set; }
        public string GlobalCode { get; set; }
        public int SortOrder { get; set; }
    }
}