using System;
using TourCore.Domain.Hotels.ValueObjects;

namespace TourCore.Application.AccommodationTypes.DTOs
{
    public class AccommodationTypeDto
    {
        public int Id { get; set; }

        public string Code { get; set; }
        public string Name { get; set; }
        public string NameEn { get; set; }

        public bool IsMain { get; set; }

        public short? AgeFrom { get; set; }
        public short? AgeTo { get; set; }

        public short? PerRoom { get; set; }
        public int SortOrder { get; set; }

        public string Description { get; set; }

        public AccommodationPlacementRule MainPlacementRule { get; set; }
        public AccommodationPlacementRule ExtraPlacementRule { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}