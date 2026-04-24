using System;

namespace TourCore.Application.Hotels.DTOs
{
    public class HotelDto
    {
        public int Id { get; set; }

        public int CountryId { get; set; }
        public int CityId { get; set; }
        public int? ResortId { get; set; }
        public int? CategoryId { get; set; }

        public string Name { get; set; }
        public string NameEn { get; set; }
        public string Stars { get; set; }
        public string Code { get; set; }

        public string Address { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }

        public string Latitude { get; set; }
        public string Longitude { get; set; }

        public bool IsCruise { get; set; }
        public int SortOrder { get; set; }
        public int? Rank { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}