namespace TourCore.Pricing.Application.Abstractions;

public interface ITransportPriceCatalogReferenceRepository
{
    Task<bool> BusTransferPointExistsAsync(int id, CancellationToken ct);
    Task<bool> RailwayTransferExistsAsync(int id, CancellationToken ct);
    Task<bool> TransportExistsAsync(int id, CancellationToken ct);
}
