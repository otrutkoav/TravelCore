using TourCore.Application.ServiceTypes.DTOs;
using TourCore.Domain.Services.Entities;

namespace TourCore.Application.ServiceTypes.Mappings
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
                Category = entity.Category,
                ControlMode = entity.ControlMode,
                IsCity = entity.IsCity,
                HasPrimaryParameter = entity.HasPrimaryParameter,
                HasSecondaryParameter = entity.HasSecondaryParameter,
                RoundGrossAmount = entity.RoundGrossAmount,
                IsDuration = entity.IsDuration,
                UseManualInput = entity.UseManualInput,
                IsQuoted = entity.IsQuoted,
                IsIndividual = entity.IsIndividual,
                SmallAmountPercent = entity.SmallAmountPercent,
                SmallAmountThreshold = entity.SmallAmountThreshold,
                UseSmallAmountAndRule = entity.UseSmallAmountAndRule,
                IsRoute = entity.IsRoute,
                IsPartnerBasedOn = entity.IsPartnerBasedOn,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }

        public static ServiceTypeListItemDto ToListItemDto(this ServiceType entity)
        {
            return new ServiceTypeListItemDto
            {
                Id = entity.Id,
                Name = entity.Name,
                NameEn = entity.NameEn,
                Code = entity.Code,
                HasPrimaryParameter = entity.HasPrimaryParameter,
                HasSecondaryParameter = entity.HasSecondaryParameter,
                IsRoute = entity.IsRoute
            };
        }
    }
}