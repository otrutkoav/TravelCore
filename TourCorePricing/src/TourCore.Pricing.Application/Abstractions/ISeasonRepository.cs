using TourCore.Pricing.Domain.Seasons;

namespace TourCore.Pricing.Application.Abstractions;

public interface ISeasonRepository
{
    Task<IReadOnlyList<Season>> ListAsync(bool? isActive, CancellationToken cancellationToken);
    Task<Season?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<bool> NameExistsAsync(string name, int? excludedId, CancellationToken cancellationToken);
    Task<bool> MainExistsAsync(int? excludedId, CancellationToken cancellationToken);
    Task AddAsync(Season season, CancellationToken cancellationToken);
    void Remove(Season season);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}
