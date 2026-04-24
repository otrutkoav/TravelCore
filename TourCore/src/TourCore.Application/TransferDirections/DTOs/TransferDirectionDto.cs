using System;

namespace TourCore.Application.TransferDirections.DTOs
{
    public class TransferDirectionDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}