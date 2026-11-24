using TourCore.Pricing.Domain.PricePackages;

namespace TourCore.Pricing.Application.Abstractions;

public interface IPricePackageRepository
{
    Task<IReadOnlyList<PricePackage>> ListAsync(
        int? countryId,
        bool? isDisabled,
        string? search,
        CancellationToken cancellationToken);

    Task<PricePackage?> GetByIdAsync(int id, bool includeDeleted, CancellationToken cancellationToken);
    Task AddAsync(PricePackage pricePackage, CancellationToken cancellationToken);
    Task<bool> NameExistsAsync(string name, int countryId, int? excludedId, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}
