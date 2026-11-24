using TourCore.Pricing.Domain.SPOTypes;

namespace TourCore.Pricing.Application.Abstractions;

public interface ISPOTypeRepository
{
    Task<IReadOnlyList<SPOType>> ListAsync(CancellationToken cancellationToken);
    Task<SPOType?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<bool> NameExistsAsync(string name, int? excludedId, CancellationToken cancellationToken);
    Task AddAsync(SPOType spoType, CancellationToken cancellationToken);
    void Remove(SPOType spoType);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}
