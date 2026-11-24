using TourCore.Pricing.Application.Abstractions;
using TourCore.Pricing.Application.Common;
using TourCore.Pricing.Contracts.RailwayPrices;
using TourCore.Pricing.Domain.RailwayPrices;
using TourCore.Pricing.Domain.TransportPrices;

namespace TourCore.Pricing.Application.RailwayPrices;

public sealed class RailwayTransferPriceService
{
    public const int RailwayTransferServiceTypeId = 15;
    private readonly IRailwayTransferPriceRepository _repository;
    private readonly ITransportPriceCatalogReferenceRepository _catalog;
    private readonly IPriceBlockRepository _blocks;
    private readonly ICurrentUser _user;
    private readonly IClock _clock;

    public RailwayTransferPriceService(IRailwayTransferPriceRepository repository,
        ITransportPriceCatalogReferenceRepository catalog, IPriceBlockRepository blocks,
        ICurrentUser user, IClock clock)
    { _repository = repository; _catalog = catalog; _blocks = blocks; _user = user; _clock = clock; }

    public async Task<IReadOnlyList<RailwayTransferPriceDto>> ListAsync(int? blockId, int? transferId, int? transportId, CancellationToken ct) =>
        (await _repository.ListAsync(blockId, transferId, transportId, ct)).Select(ToDto).ToArray();
    public async Task<RailwayTransferPriceDto> GetAsync(int id, CancellationToken ct) => ToDto(await RequiredAsync(id, ct));

    public async Task<RailwayTransferPriceDto> CreateAsync(CreateRailwayTransferPriceRequest request, CancellationToken ct)
    {
        ArgumentNullException.ThrowIfNull(request);
        await ValidateReferencesAsync(request, ct);
        var price = Create(request, _user.UserId, _clock.UtcNow);
        await _repository.AddAsync(price, ct);
        await _repository.SaveChangesAsync(ct);
        return ToDto(price);
    }

    public async Task<RailwayTransferPriceDto> UpdateAsync(int id, UpdateRailwayTransferPriceRequest request, CancellationToken ct)
    {
        ArgumentNullException.ThrowIfNull(request);
        var price = await RequiredAsync(id, ct);
        await ValidateReferencesAsync(request, ct);
        price.Update(request.PriceBlockId, request.RailwayTransferId, request.TransportId,
            request.TravelDateFrom, request.TravelDateTo, request.DaysOfWeekMask,
            request.SaleDateFrom, request.SaleDateTo, request.AgeFrom, request.AgeTo,
            (TransportPriceScope)request.Scope, (TransportPricePeriodUnit)request.PeriodUnit,
            request.NettoAmount, request.BruttoAmount, request.CurrencyId, request.Priority, _clock.UtcNow);
        await _repository.SaveChangesAsync(ct);
        return ToDto(price);
    }

    public async Task DeleteAsync(int id, CancellationToken ct)
    { _repository.Remove(await RequiredAsync(id, ct)); await _repository.SaveChangesAsync(ct); }

    private async Task ValidateReferencesAsync(RailwayTransferPriceWriteRequest request, CancellationToken ct)
    {
        var block = await _blocks.GetByIdAsync(request.PriceBlockId, ct) ?? throw new NotFoundException(PricingErrorMessages.PriceBlockNotFound(request.PriceBlockId), PricingErrorCodes.PriceBlockNotFound);
        if (block.ServiceTypeId != RailwayTransferServiceTypeId) throw new ValidationException("Для цены ЖД-переезда требуется ценовой блок с типом услуги «ЖД-переезд».");
        if (!await _catalog.RailwayTransferExistsAsync(request.RailwayTransferId, ct)) throw new NotFoundException(PricingErrorMessages.RailwayTransferNotFound(request.RailwayTransferId), PricingErrorCodes.RailwayTransferNotFound);
        if (!await _catalog.TransportExistsAsync(request.TransportId, ct)) throw new NotFoundException(PricingErrorMessages.TransportNotFound(request.TransportId), PricingErrorCodes.TransportNotFound);
    }

    private async Task<RailwayTransferPrice> RequiredAsync(int id, CancellationToken ct) =>
        await _repository.GetByIdAsync(id, ct) ?? throw new NotFoundException($"Railway transfer price '{id}' was not found.", "railway_transfer_price_not_found");

    private static RailwayTransferPrice Create(RailwayTransferPriceWriteRequest r, string user, DateTime now) =>
        new(r.PriceBlockId, r.RailwayTransferId, r.TransportId, r.TravelDateFrom, r.TravelDateTo,
            r.DaysOfWeekMask, r.SaleDateFrom, r.SaleDateTo, r.AgeFrom, r.AgeTo,
            (TransportPriceScope)r.Scope, (TransportPricePeriodUnit)r.PeriodUnit,
            r.NettoAmount, r.BruttoAmount, r.CurrencyId, r.Priority, user, now);

    private static RailwayTransferPriceDto ToDto(RailwayTransferPrice x) => new()
    {
        Id=x.Id, PriceBlockId=x.PriceBlockId, RailwayTransferId=x.RailwayTransferId,
        TransportId=x.TransportId, TravelDateFrom=x.TravelDateFrom, TravelDateTo=x.TravelDateTo,
        DaysOfWeekMask=x.DaysOfWeekMask, SaleDateFrom=x.SaleDateFrom, SaleDateTo=x.SaleDateTo,
        AgeFrom=x.AgeFrom, AgeTo=x.AgeTo, Scope=(byte)x.Scope, PeriodUnit=(byte)x.PeriodUnit,
        NettoAmount=x.NettoAmount, BruttoAmount=x.BruttoAmount, CurrencyId=x.CurrencyId,
        Priority=x.Priority, CreatedByUserId=x.CreatedByUserId, CreatedAt=x.CreatedAt, UpdatedAt=x.UpdatedAt
    };
}
