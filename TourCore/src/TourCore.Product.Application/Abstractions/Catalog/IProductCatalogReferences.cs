using System.Threading;
using System.Threading.Tasks;

namespace TourCore.Product.Application.Abstractions.Catalog;

/// <summary>
/// Product-owned port for validating identifiers managed by Catalog.
/// The Product application does not depend on Catalog repositories or entities.
/// </summary>
public interface IProductCatalogReferences
{
    Task<bool> CurrencyExistsAsync(int currencyId, CancellationToken cancellationToken);
    Task<bool> ServiceTypeExistsAsync(int serviceTypeId, CancellationToken cancellationToken);
}
