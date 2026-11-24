using System.Data;
using Microsoft.EntityFrameworkCore;
using TourCore.Pricing.Application.Abstractions;

namespace TourCore.Pricing.Infrastructure.Postgres.Persistence;

internal sealed class FlightCatalogReferenceRepository : IFlightCatalogReferenceRepository
{
    private readonly PricingDbContext _db;
    public FlightCatalogReferenceRepository(PricingDbContext db) { _db = db; }

    public Task<bool> CharterExistsAsync(int id, CancellationToken ct) => ExistsAsync("Charters", id, ct);
    public Task<bool> AirClassExistsAsync(int id, CancellationToken ct) => ExistsAsync("AirClasses", id, ct);

    private async Task<bool> ExistsAsync(string table, int id, CancellationToken ct)
    {
        var connection = _db.Database.GetDbConnection();
        var closeAfter = connection.State != ConnectionState.Open;
        if (closeAfter) await connection.OpenAsync(ct);
        try
        {
            await using var command = connection.CreateCommand();
            command.CommandText = $"SELECT EXISTS (SELECT 1 FROM \"{table}\" WHERE \"Id\" = @id)";
            var parameter = command.CreateParameter();
            parameter.ParameterName = "id";
            parameter.Value = id;
            command.Parameters.Add(parameter);
            return (bool)(await command.ExecuteScalarAsync(ct) ?? false);
        }
        finally
        {
            if (closeAfter) await connection.CloseAsync();
        }
    }
}
