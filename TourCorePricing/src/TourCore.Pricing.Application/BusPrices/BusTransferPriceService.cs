using TourCore.Pricing.Application.Abstractions;
using TourCore.Pricing.Application.Common;
using TourCore.Pricing.Contracts.BusPrices;
using TourCore.Pricing.Domain.BusPrices;
using TourCore.Pricing.Domain.TransportPrices;

namespace TourCore.Pricing.Application.BusPrices;

public sealed class BusTransferPriceService
{
    public const int BusTransferServiceTypeId = 14;
    private readonly IBusTransferPriceRepository _repository;
    private readonly ITransportPriceCatalogReferenceRepository _catalog;
    private readonly IPriceBlockRepository _blocks;
    private readonly ICurrentUser _user;
    private readonly IClock _clock;

    public BusTransferPriceService(IBusTransferPriceRepository repository,
        ITransportPriceCatalogReferenceRepository catalog, IPriceBlockRepository blocks,
        ICurrentUser user, IClock clock)
    { _repository = repository; _catalog = catalog; _blocks = blocks; _user = user; _clock = clock; }

    public async Task<IReadOnlyList<BusTransferPriceDto>> ListAsync(int? blockId, int? pointId, int? transportId, CancellationToken ct) =>
        (await _repository.ListAsync(blockId, pointId, transportId, ct)).Select(ToDto).ToArray();
    public async Task<BusTransferPriceDto> GetAsync(int id, CancellationToken ct) => ToDto(await RequiredAsync(id, ct));

    public async Task<BusTransferPriceDto> CreateAsync(CreateBusTransferPriceRequest request, CancellationToken ct)
    {
        ArgumentNullException.ThrowIfNull(request);
        await ValidateReferencesAsync(request, ct);
        var price = Create(request, _user.UserId, _clock.UtcNow);
        await _repository.AddAsync(price, ct);
        await _repository.SaveChangesAsync(ct);
        return ToDto(price);
    }

    public async Task<BusTransferPriceDto> UpdateAsync(int id, UpdateBusTransferPriceRequest request, CancellationToken ct)
    {
        ArgumentNullException.ThrowIfNull(request);
        var price = await RequiredAsync(id, ct);
        await ValidateReferencesAsync(request, ct);
        price.Update(request.PriceBlockId, request.BusTransferPointId, request.TransportId,
            request.TravelDateFrom, request.TravelDateTo, request.DaysOfWeekMask,
            request.SaleDateFrom, request.SaleDateTo, request.AgeFrom, request.AgeTo,
            request.PassengerCountFrom, request.PassengerCountTo, (TransportPriceScope)request.Scope,
            (TransportPricePeriodUnit)request.PeriodUnit, request.NettoAmount, request.BruttoAmount,
            request.CurrencyId, request.Priority, _clock.UtcNow);
        await _repository.SaveChangesAsync(ct);
        return ToDto(price);
    }

    public async Task DeleteAsync(int id, CancellationToken ct)
    { _repository.Remove(await RequiredAsync(id, ct)); await _repository.SaveChangesAsync(ct); }

    private async Task ValidateReferencesAsync(BusTransferPriceWriteRequest request, CancellationToken ct)
    {
        var block = await _blocks.GetByIdAsync(request.PriceBlockId, ct) ?? throw new NotFoundException(PricingErrorMessages.PriceBlockNotFound(request.PriceBlockId), PricingErrorCodes.PriceBlockNotFound);
        if (block.ServiceTypeId != BusTransferServiceTypeId) throw new ValidationException("Для цены автобусного переезда требуется ценовой блок с типом услуги «Автобусный переезд».");
        if (!await _catalog.BusTransferPointExistsAsync(request.BusTransferPointId, ct)) throw new NotFoundException(PricingErrorMessages.BusTransferPointNotFound(request.BusTransferPointId), PricingErrorCodes.BusTransferPointNotFound);
        if (!await _catalog.TransportExistsAsync(request.TransportId, ct)) throw new NotFoundException(PricingErrorMessages.TransportNotFound(request.TransportId), PricingErrorCodes.TransportNotFound);
    }

    private async Task<BusTransferPrice> RequiredAsync(int id, CancellationToken ct) =>
        await _repository.GetByIdAsync(id, ct) ?? throw new NotFoundException($"Bus transfer price '{id}' was not found.", "bus_transfer_price_not_found");

    private static BusTransferPrice Create(BusTransferPriceWriteRequest r, string user, DateTime now) =>
        new(r.PriceBlockId, r.BusTransferPointId, r.TransportId, r.TravelDateFrom, r.TravelDateTo,
            r.DaysOfWeekMask, r.SaleDateFrom, r.SaleDateTo, r.AgeFrom, r.AgeTo,
            r.PassengerCountFrom, r.PassengerCountTo, (TransportPriceScope)r.Scope,
            (TransportPricePeriodUnit)r.PeriodUnit, r.NettoAmount, r.BruttoAmount,
            r.CurrencyId, r.Priority, user, now);

    private static BusTransferPriceDto ToDto(BusTransferPrice x) => new()
    {
        Id=x.Id, PriceBlockId=x.PriceBlockId, BusTransferPointId=x.BusTransferPointId,
        TransportId=x.TransportId, TravelDateFrom=x.TravelDateFrom, TravelDateTo=x.TravelDateTo,
        DaysOfWeekMask=x.DaysOfWeekMask, SaleDateFrom=x.SaleDateFrom, SaleDateTo=x.SaleDateTo,
        AgeFrom=x.AgeFrom, AgeTo=x.AgeTo, PassengerCountFrom=x.PassengerCountFrom,
        PassengerCountTo=x.PassengerCountTo, Scope=(byte)x.Scope, PeriodUnit=(byte)x.PeriodUnit,
        NettoAmount=x.NettoAmount, BruttoAmount=x.BruttoAmount, CurrencyId=x.CurrencyId,
        Priority=x.Priority, CreatedByUserId=x.CreatedByUserId, CreatedAt=x.CreatedAt, UpdatedAt=x.UpdatedAt
    };
}
