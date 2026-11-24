using Microsoft.EntityFrameworkCore;
using TourCore.Pricing.Application.Abstractions;
using TourCore.Pricing.Domain.PricePackages;

namespace TourCore.Pricing.Infrastructure.Postgres.Persistence;

internal sealed class PricePackageRepository : IPricePackageRepository
{
    private readonly PricingDbContext _dbContext;

    public PricePackageRepository(PricingDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<PricePackage>> ListAsync(
        int? countryId,
        bool? isDisabled,
        string? search,
        CancellationToken cancellationToken)
    {
        var query = _dbContext.PricePackages
            .AsNoTracking()
            .Where(pricePackage => !pricePackage.Deleted);

        if (countryId.HasValue)
            query = query.Where(pricePackage => pricePackage.CountryId == countryId.Value);
        if (isDisabled.HasValue)
            query = query.Where(pricePackage => pricePackage.IsDisabled == isDisabled.Value);
        if (!string.IsNullOrWhiteSpace(search))
        {
            var pattern = $"%{search.Trim()}%";
            query = query.Where(pricePackage => EF.Functions.ILike(pricePackage.Name, pattern));
        }

        return await query
            .OrderBy(pricePackage => pricePackage.Name)
            .ThenBy(pricePackage => pricePackage.Id)
            .ToArrayAsync(cancellationToken);
    }

    public Task<PricePackage?> GetByIdAsync(
        int id,
        bool includeDeleted,
        CancellationToken cancellationToken)
    {
        return _dbContext.PricePackages.FirstOrDefaultAsync(
            pricePackage => pricePackage.Id == id && (includeDeleted || !pricePackage.Deleted),
            cancellationToken);
    }

    public async Task AddAsync(PricePackage pricePackage, CancellationToken cancellationToken)
    {
        await _dbContext.PricePackages.AddAsync(pricePackage, cancellationToken);
    }

    public Task<bool> NameExistsAsync(
        string name,
        int countryId,
        int? excludedId,
        CancellationToken cancellationToken)
    {
        var normalizedName = name.ToLower();
        return _dbContext.PricePackages.AnyAsync(
            pricePackage =>
                !pricePackage.Deleted
                && pricePackage.CountryId == countryId
                && pricePackage.Name.ToLower() == normalizedName
                && (!excludedId.HasValue || pricePackage.Id != excludedId.Value),
            cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
