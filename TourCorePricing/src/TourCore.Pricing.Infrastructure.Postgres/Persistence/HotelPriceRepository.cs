using Microsoft.EntityFrameworkCore;
using TourCore.Pricing.Application.Abstractions;
using TourCore.Pricing.Domain.HotelPrices;

namespace TourCore.Pricing.Infrastructure.Postgres.Persistence;

internal sealed class HotelPriceRepository : IHotelPriceRepository
{
    private readonly PricingDbContext _db;
    public HotelPriceRepository(PricingDbContext db) { _db = db; }

    public async Task<IReadOnlyList<HotelPriceRule>> ListRulesAsync(int? blockId, int? hotelId, CancellationToken ct)
    {
        var query = _db.HotelPriceRules.AsNoTracking();
        if (blockId.HasValue) query = query.Where(x => x.PriceBlockId == blockId.Value);
        if (hotelId.HasValue) query = query.Where(x => x.HotelId == hotelId.Value);
        return await query.OrderByDescending(x => x.Priority).ThenBy(x => x.Id).ToArrayAsync(ct);
    }
    public Task<HotelPriceRule?> GetRuleAsync(int id, CancellationToken ct) => _db.HotelPriceRules.FirstOrDefaultAsync(x => x.Id == id, ct);
    public async Task AddRuleAsync(HotelPriceRule rule, CancellationToken ct) => await _db.HotelPriceRules.AddAsync(rule, ct);
    public void RemoveRule(HotelPriceRule rule) => _db.HotelPriceRules.Remove(rule);

    public async Task<IReadOnlyList<HotelPriceComponent>> ListComponentsAsync(int ruleId, CancellationToken ct) =>
        await _db.HotelPriceComponents.AsNoTracking().Where(x => x.HotelPriceRuleId == ruleId).OrderBy(x => x.Id).ToArrayAsync(ct);
    public Task<bool> HasComponentsAsync(int ruleId, CancellationToken ct) =>
        _db.HotelPriceComponents.AnyAsync(x => x.HotelPriceRuleId == ruleId, ct);
    public Task<HotelPriceComponent?> GetComponentAsync(int id, CancellationToken ct) => _db.HotelPriceComponents.FirstOrDefaultAsync(x => x.Id == id, ct);
    public async Task AddComponentAsync(HotelPriceComponent item, CancellationToken ct) => await _db.HotelPriceComponents.AddAsync(item, ct);
    public void RemoveComponent(HotelPriceComponent item) => _db.HotelPriceComponents.Remove(item);

    public async Task<IReadOnlyList<HotelMealPriceRule>> ListMealRulesAsync(int? blockId, int? hotelId, CancellationToken ct)
    {
        var query = _db.HotelMealPriceRules.AsNoTracking();
        if (blockId.HasValue) query = query.Where(x => x.PriceBlockId == blockId.Value);
        if (hotelId.HasValue) query = query.Where(x => x.HotelId == hotelId.Value);
        return await query.OrderByDescending(x => x.Priority).ThenBy(x => x.Id).ToArrayAsync(ct);
    }
    public Task<HotelMealPriceRule?> GetMealRuleAsync(int id, CancellationToken ct) => _db.HotelMealPriceRules.FirstOrDefaultAsync(x => x.Id == id, ct);
    public async Task AddMealRuleAsync(HotelMealPriceRule item, CancellationToken ct) => await _db.HotelMealPriceRules.AddAsync(item, ct);
    public void RemoveMealRule(HotelMealPriceRule item) => _db.HotelMealPriceRules.Remove(item);
    public Task SaveChangesAsync(CancellationToken ct) => _db.SaveChangesAsync(ct);
}
