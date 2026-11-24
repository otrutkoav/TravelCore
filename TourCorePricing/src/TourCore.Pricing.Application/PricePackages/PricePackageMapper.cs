using TourCore.Pricing.Contracts.PricePackages;
using TourCore.Pricing.Domain.PricePackages;

namespace TourCore.Pricing.Application.PricePackages;

internal static class PricePackageMapper
{
    public static PricePackageDto ToDto(this PricePackage pricePackage)
    {
        return new PricePackageDto
        {
            Id = pricePackage.Id,
            Name = pricePackage.Name,
            CountryId = pricePackage.CountryId,
            Deleted = pricePackage.Deleted,
            IsDisabled = pricePackage.IsDisabled,
            CreatedByUserId = pricePackage.CreatedByUserId,
            CreatedAt = pricePackage.CreatedAt,
            UpdatedAt = pricePackage.UpdatedAt
        };
    }
}
