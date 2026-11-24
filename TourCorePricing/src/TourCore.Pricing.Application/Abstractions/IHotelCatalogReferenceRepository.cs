namespace TourCore.Pricing.Application.Abstractions;

public interface IHotelCatalogReferenceRepository
{
    Task<bool> HotelExistsAsync(int id, CancellationToken cancellationToken);
    Task<bool> RoomTypeExistsAsync(int id, CancellationToken cancellationToken);
    Task<bool> RoomCategoryExistsAsync(int id, CancellationToken cancellationToken);
    Task<bool> AccommodationTypeExistsAsync(int id, CancellationToken cancellationToken);
    Task<bool> MealTypeExistsAsync(int id, CancellationToken cancellationToken);
}
