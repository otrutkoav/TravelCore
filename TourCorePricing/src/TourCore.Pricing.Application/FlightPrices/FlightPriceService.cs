using TourCore.Pricing.Application.Abstractions;
using TourCore.Pricing.Application.Common;
using TourCore.Pricing.Contracts.FlightPrices;
using TourCore.Pricing.Domain.FlightPrices;

namespace TourCore.Pricing.Application.FlightPrices;

public sealed class FlightPriceService
{
    public const int FlightServiceTypeId = 1;
    private readonly IFlightPriceRepository _repository;
    private readonly IFlightCatalogReferenceRepository _catalog;
    private readonly IPriceBlockRepository _priceBlocks;
    private readonly ICurrentUser _currentUser;
    private readonly IClock _clock;

    public FlightPriceService(IFlightPriceRepository repository,
        IFlightCatalogReferenceRepository catalog, IPriceBlockRepository priceBlocks,
        ICurrentUser currentUser, IClock clock)
    {
        _repository = repository;
        _catalog = catalog;
        _priceBlocks = priceBlocks;
        _currentUser = currentUser;
        _clock = clock;
    }

    public async Task<IReadOnlyList<FlightPriceDto>> ListAsync(
        int? priceBlockId, int? charterId, int? airClassId, CancellationToken ct) =>
        (await _repository.ListAsync(priceBlockId, charterId, airClassId, ct)).Select(ToDto).ToArray();

    public async Task<FlightPriceDto> GetAsync(int id, CancellationToken ct) =>
        ToDto(await GetRequiredAsync(id, ct));

    public async Task<FlightPriceDto> CreateAsync(CreateFlightPriceRequest request, CancellationToken ct)
    {
        ArgumentNullException.ThrowIfNull(request);
        await ValidateReferencesAsync(request, ct);
        var price = Create(request, _currentUser.UserId, _clock.UtcNow);
        await _repository.AddAsync(price, ct);
        await _repository.SaveChangesAsync(ct);
        return ToDto(price);
    }

    public async Task<FlightPriceDto> UpdateAsync(int id, UpdateFlightPriceRequest request, CancellationToken ct)
    {
        ArgumentNullException.ThrowIfNull(request);
        var price = await GetRequiredAsync(id, ct);
        await ValidateReferencesAsync(request, ct);
        price.Update(request.PriceBlockId, request.CharterId, request.AirClassId,
            request.TravelDateFrom, request.TravelDateTo, request.DaysOfWeekMask,
            request.SaleDateFrom, request.SaleDateTo, request.AgeFrom, request.AgeTo,
            request.NettoAmount, request.BruttoAmount, request.CurrencyId, request.Priority,
            _clock.UtcNow);
        await _repository.SaveChangesAsync(ct);
        return ToDto(price);
    }

    public async Task DeleteAsync(int id, CancellationToken ct)
    {
        _repository.Remove(await GetRequiredAsync(id, ct));
        await _repository.SaveChangesAsync(ct);
    }

    private async Task ValidateReferencesAsync(FlightPriceWriteRequest request, CancellationToken ct)
    {
        var block = await _priceBlocks.GetByIdAsync(request.PriceBlockId, ct)
            ?? throw new NotFoundException(PricingErrorMessages.PriceBlockNotFound(request.PriceBlockId), PricingErrorCodes.PriceBlockNotFound);
        if (block.ServiceTypeId != FlightServiceTypeId)
            throw new ValidationException("Для цены авиаперелёта требуется ценовой блок с типом услуги «Авиаперелёт».");
        if (!await _catalog.CharterExistsAsync(request.CharterId, ct))
            throw new NotFoundException(PricingErrorMessages.CharterNotFound(request.CharterId), PricingErrorCodes.CharterNotFound);
        if (!await _catalog.AirClassExistsAsync(request.AirClassId, ct))
            throw new NotFoundException(PricingErrorMessages.AirClassNotFound(request.AirClassId), PricingErrorCodes.AirClassNotFound);
    }

    private async Task<FlightPrice> GetRequiredAsync(int id, CancellationToken ct) =>
        await _repository.GetByIdAsync(id, ct)
        ?? throw new NotFoundException($"Flight price '{id}' was not found.", "flight_price_not_found");

    private static FlightPrice Create(FlightPriceWriteRequest request, string userId, DateTime now) =>
        new(request.PriceBlockId, request.CharterId, request.AirClassId,
            request.TravelDateFrom, request.TravelDateTo, request.DaysOfWeekMask,
            request.SaleDateFrom, request.SaleDateTo, request.AgeFrom, request.AgeTo,
            request.NettoAmount, request.BruttoAmount, request.CurrencyId, request.Priority,
            userId, now);

    private static FlightPriceDto ToDto(FlightPrice x) => new()
    {
        Id = x.Id,
        PriceBlockId = x.PriceBlockId,
        CharterId = x.CharterId,
        AirClassId = x.AirClassId,
        TravelDateFrom = x.TravelDateFrom,
        TravelDateTo = x.TravelDateTo,
        DaysOfWeekMask = x.DaysOfWeekMask,
        SaleDateFrom = x.SaleDateFrom,
        SaleDateTo = x.SaleDateTo,
        AgeFrom = x.AgeFrom,
        AgeTo = x.AgeTo,
        NettoAmount = x.NettoAmount,
        BruttoAmount = x.BruttoAmount,
        CurrencyId = x.CurrencyId,
        Priority = x.Priority,
        CreatedByUserId = x.CreatedByUserId,
        CreatedAt = x.CreatedAt,
        UpdatedAt = x.UpdatedAt
    };
}
