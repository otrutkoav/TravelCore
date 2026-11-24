using System.Data;
using Microsoft.EntityFrameworkCore;
using TourCore.Pricing.Application.Abstractions;

namespace TourCore.Pricing.Infrastructure.Postgres.Persistence;

internal sealed class HotelCatalogReferenceRepository : IHotelCatalogReferenceRepository
{
    private readonly PricingDbContext _db;
    public HotelCatalogReferenceRepository(PricingDbContext db) { _db = db; }

    public Task<bool> HotelExistsAsync(int id, CancellationToken ct) => ExistsAsync("Hotels", id, ct);
    public Task<bool> RoomTypeExistsAsync(int id, CancellationToken ct) => ExistsAsync("RoomTypes", id, ct);
    public Task<bool> RoomCategoryExistsAsync(int id, CancellationToken ct) => ExistsAsync("RoomCategories", id, ct);
    public Task<bool> AccommodationTypeExistsAsync(int id, CancellationToken ct) => ExistsAsync("AccommodationTypes", id, ct);
    public Task<bool> MealTypeExistsAsync(int id, CancellationToken ct) => ExistsAsync("MealTypes", id, ct);

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
