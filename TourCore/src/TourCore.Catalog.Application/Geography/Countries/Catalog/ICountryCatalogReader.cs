using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Contracts.Geography.Countries;

namespace TourCore.Application.Geography.Countries.Catalog
{
    public interface ICountryCatalogReader
    {
        Task<IReadOnlyCollection<CountryListItemDto>> GetAllAsync(
            CancellationToken cancellationToken);
    }
}