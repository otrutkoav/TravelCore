namespace TourCore.Pricing.Application.Abstractions;

public interface IFlightCatalogReferenceRepository
{
    Task<bool> CharterExistsAsync(int id, CancellationToken ct);
    Task<bool> AirClassExistsAsync(int id, CancellationToken ct);
}
