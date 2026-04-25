using TourCore.Contracts.Transportation.Transports;
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
                ShowOrder = entity.ShowOrder
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