using System.Data;
using Microsoft.EntityFrameworkCore;
using TourCore.Pricing.Application.Abstractions;

namespace TourCore.Pricing.Infrastructure.Postgres.Persistence;

internal sealed class TransportPriceCatalogReferenceRepository : ITransportPriceCatalogReferenceRepository
{
    private readonly PricingDbContext _db;
    public TransportPriceCatalogReferenceRepository(PricingDbContext db) { _db = db; }
    public Task<bool> BusTransferPointExistsAsync(int id, CancellationToken ct) => ExistsAsync("BusTransferPoints", id, ct);
    public Task<bool> RailwayTransferExistsAsync(int id, CancellationToken ct) => ExistsAsync("RailwayTransfers", id, ct);
    public Task<bool> TransportExistsAsync(int id, CancellationToken ct) => ExistsAsync("Transports", id, ct);
    private async Task<bool> ExistsAsync(string table, int id, CancellationToken ct)
    {
        var connection = _db.Database.GetDbConnection();
        var closeAfter = connection.State != ConnectionState.Open;
        if (closeAfter) await connection.OpenAsync(ct);
        try
        {
            await using var command = connection.CreateCommand();
            command.CommandText = $"SELECT EXISTS (SELECT 1 FROM \"{table}\" WHERE \"Id\" = @id)";
            var parameter = command.CreateParameter(); parameter.ParameterName = "id"; parameter.Value = id; command.Parameters.Add(parameter);
            return (bool)(await command.ExecuteScalarAsync(ct) ?? false);
        }
        finally { if (closeAfter) await connection.CloseAsync(); }
    }
}
