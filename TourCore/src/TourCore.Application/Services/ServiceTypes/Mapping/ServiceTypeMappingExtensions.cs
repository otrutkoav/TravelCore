using TourCore.Domain.Services.Entities;
using TourCore.Contracts.Services.ServiceTypes;

namespace TourCore.Application.Services.ServiceTypes.Mapping
{
    public static class ServiceTypeMappingExtensions
    {
        public static ServiceTypeDto ToDto(this ServiceType entity)
        {
            return new ServiceTypeDto
            {
                Id = entity.Id,
                Name = entity.Name,
                NameEn = entity.NameEn,
                Code = entity.Code,
                IsDuration = entity.IsDuration,
                IsRoute = entity.IsRoute,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }
    }
}
