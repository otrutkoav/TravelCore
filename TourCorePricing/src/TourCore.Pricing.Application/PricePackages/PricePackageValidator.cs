using TourCore.Pricing.Application.Common;
using TourCore.Pricing.Domain.PricePackages;

namespace TourCore.Pricing.Application.PricePackages;

internal static class PricePackageValidator
{
    public static void Validate(string? name, int countryId)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(name))
            errors[nameof(name)] = new[] { "Price package name is required." };
        else if (name.Trim().Length > PricePackage.MaximumNameLength)
            errors[nameof(name)] = new[] { $"Price package name must be {PricePackage.MaximumNameLength} characters or less." };

        if (countryId <= 0)
            errors[nameof(countryId)] = new[] { "Country identifier must be greater than zero." };

        if (errors.Count > 0)
            throw new ValidationException(errors);
    }
}
