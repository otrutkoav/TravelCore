using TourCore.Application.Transports.DTOs;
using TourCore.Domain.Transportation.Entities;

namespace TourCore.Application.Transports.Mappings
{
    public static class TransportMappingExtensions
    {
        public static TransportDto ToDto(this Transport entity)
        {
            return new TransportDto
            {
                Id = entity.Id,
                Name = entity.Name,
                NameEn = entity.NameEn,
                SeatsCount = entity.SeatsCount,
                ShowOrder = entity.ShowOrder,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }

        public static TransportListItemDto ToListItemDto(this Transport entity)
        {
            return new TransportListItemDto
            {
                Id = entity.Id,
                Name = entity.Name,
                NameEn = entity.NameEn,
                SeatsCount = entity.SeatsCount,
                ShowOrder = entity.ShowOrder
            };
        }
    }
}