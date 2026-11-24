using TourCore.Pricing.Domain.HotelPrices;

namespace TourCore.Pricing.Application.Abstractions;

public interface IHotelPriceRepository
{
    Task<IReadOnlyList<HotelPriceRule>> ListRulesAsync(int? priceBlockId, int? hotelId, CancellationToken cancellationToken);
    Task<HotelPriceRule?> GetRuleAsync(int id, CancellationToken cancellationToken);
    Task AddRuleAsync(HotelPriceRule rule, CancellationToken cancellationToken);
    void RemoveRule(HotelPriceRule rule);

    Task<IReadOnlyList<HotelPriceComponent>> ListComponentsAsync(int hotelPriceRuleId, CancellationToken cancellationToken);
    Task<bool> HasComponentsAsync(int hotelPriceRuleId, CancellationToken cancellationToken);
    Task<HotelPriceComponent?> GetComponentAsync(int id, CancellationToken cancellationToken);
    Task AddComponentAsync(HotelPriceComponent component, CancellationToken cancellationToken);
    void RemoveComponent(HotelPriceComponent component);

    Task<IReadOnlyList<HotelMealPriceRule>> ListMealRulesAsync(int? priceBlockId, int? hotelId, CancellationToken cancellationToken);
    Task<HotelMealPriceRule?> GetMealRuleAsync(int id, CancellationToken cancellationToken);
    Task AddMealRuleAsync(HotelMealPriceRule rule, CancellationToken cancellationToken);
    void RemoveMealRule(HotelMealPriceRule rule);

    Task SaveChangesAsync(CancellationToken cancellationToken);
}
