using System;

namespace TourCore.Application.HotelCategories.DTOs
{
    public class HotelCategoryDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string NameEn { get; set; }
        public int? PrintOrder { get; set; }
        public string GlobalCode { get; set; }
        public string Description { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}