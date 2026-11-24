using Microsoft.EntityFrameworkCore;
using TourCore.Pricing.Application.Abstractions;
using TourCore.Pricing.Domain.Seasons;

namespace TourCore.Pricing.Infrastructure.Postgres.Persistence;

internal sealed class SeasonRepository : ISeasonRepository
{
    private readonly PricingDbContext _dbContext;

    public SeasonRepository(PricingDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<Season>> ListAsync(bool? isActive, CancellationToken cancellationToken)
    {
        var query = _dbContext.Seasons.AsNoTracking();
        if (isActive.HasValue)
            query = query.Where(season => season.IsActive == isActive.Value);

        return await query.OrderBy(season => season.Id).ToArrayAsync(cancellationToken);
    }

    public Task<Season?> GetByIdAsync(int id, CancellationToken cancellationToken) =>
        _dbContext.Seasons.FirstOrDefaultAsync(season => season.Id == id, cancellationToken);

    public Task<bool> NameExistsAsync(string name, int? excludedId, CancellationToken cancellationToken)
    {
        var normalizedName = name.ToLower();
        return _dbContext.Seasons.AnyAsync(
            season => season.Name.ToLower() == normalizedName
                && (!excludedId.HasValue || season.Id != excludedId.Value),
            cancellationToken);
    }

    public Task<bool> MainExistsAsync(int? excludedId, CancellationToken cancellationToken) =>
        _dbContext.Seasons.AnyAsync(
            season => season.IsMain && (!excludedId.HasValue || season.Id != excludedId.Value),
            cancellationToken);

    public async Task AddAsync(Season season, CancellationToken cancellationToken) =>
        await _dbContext.Seasons.AddAsync(season, cancellationToken);

    public void Remove(Season season) => _dbContext.Seasons.Remove(season);

    public Task SaveChangesAsync(CancellationToken cancellationToken) =>
        _dbContext.SaveChangesAsync(cancellationToken);
}
