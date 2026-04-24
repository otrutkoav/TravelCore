using System;

namespace TourCore.Application.MealTypes.DTOs
{
    public class MealTypeDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string NameEn { get; set; }
        public string Code { get; set; }
        public string GlobalCode { get; set; }
        public int SortOrder { get; set; }
        public string Description { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}