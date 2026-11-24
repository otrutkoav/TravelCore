using Microsoft.EntityFrameworkCore;
using TourCore.Pricing.Application.Abstractions;
using TourCore.Pricing.Domain.RailwayPrices;

namespace TourCore.Pricing.Infrastructure.Postgres.Persistence;

internal sealed class RailwayTransferPriceRepository : IRailwayTransferPriceRepository
{
    private readonly PricingDbContext _db;
    public RailwayTransferPriceRepository(PricingDbContext db) { _db = db; }
    public async Task<IReadOnlyList<RailwayTransferPrice>> ListAsync(int? blockId, int? transferId, int? transportId, CancellationToken ct)
    {
        var query = _db.RailwayTransferPrices.AsNoTracking();
        if (blockId.HasValue) query = query.Where(x => x.PriceBlockId == blockId.Value);
        if (transferId.HasValue) query = query.Where(x => x.RailwayTransferId == transferId.Value);
        if (transportId.HasValue) query = query.Where(x => x.TransportId == transportId.Value);
        return await query.OrderByDescending(x => x.Priority).ThenBy(x => x.Id).ToArrayAsync(ct);
    }
    public Task<RailwayTransferPrice?> GetByIdAsync(int id, CancellationToken ct) => _db.RailwayTransferPrices.FirstOrDefaultAsync(x => x.Id == id, ct);
    public async Task AddAsync(RailwayTransferPrice price, CancellationToken ct) => await _db.RailwayTransferPrices.AddAsync(price, ct);
    public void Remove(RailwayTransferPrice price) => _db.RailwayTransferPrices.Remove(price);
    public Task SaveChangesAsync(CancellationToken ct) => _db.SaveChangesAsync(ct);
}
