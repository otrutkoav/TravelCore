using Microsoft.EntityFrameworkCore;
using TourCore.Product.Application.Abstractions.Catalog;

namespace TourCore.Infrastructure.Postgres.Persistence;

public sealed class ProductCatalogReferences(TourCoreDbContext dbContext)
    : IProductCatalogReferences
{
    public Task<bool> CurrencyExistsAsync(int currencyId, CancellationToken cancellationToken) =>
        dbContext.Rates.AnyAsync(rate => rate.Id == currencyId, cancellationToken);

    public Task<bool> ServiceTypeExistsAsync(int serviceTypeId, CancellationToken cancellationToken) =>
        dbContext.ServiceTypes.AnyAsync(serviceType => serviceType.Id == serviceTypeId, cancellationToken);
}
