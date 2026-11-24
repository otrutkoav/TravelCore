using TourCore.Pricing.Application.Abstractions;
using TourCore.Pricing.Application.Common;
using TourCore.Pricing.Contracts.HotelPrices;
using TourCore.Pricing.Domain.HotelPrices;

namespace TourCore.Pricing.Application.HotelPrices;

public sealed class HotelPriceService
{
    public const int HotelServiceTypeId = 3;
    private readonly IHotelPriceRepository _repository;
    private readonly IHotelCatalogReferenceRepository _catalog;
    private readonly IPriceBlockRepository _priceBlocks;
    private readonly ICurrentUser _currentUser;
    private readonly IClock _clock;

    public HotelPriceService(IHotelPriceRepository repository, IHotelCatalogReferenceRepository catalog,
        IPriceBlockRepository priceBlocks, ICurrentUser currentUser, IClock clock)
    {
        _repository = repository;
        _catalog = catalog;
        _priceBlocks = priceBlocks;
        _currentUser = currentUser;
        _clock = clock;
    }

    public async Task<IReadOnlyList<HotelPriceRuleDto>> ListRulesAsync(
        int? priceBlockId, int? hotelId, CancellationToken cancellationToken) =>
        (await _repository.ListRulesAsync(priceBlockId, hotelId, cancellationToken)).Select(ToDto).ToArray();

    public async Task<HotelPriceRuleDto> GetRuleAsync(int id, CancellationToken cancellationToken) =>
        ToDto(await GetRequiredRuleAsync(id, cancellationToken));

    public async Task<HotelPriceRuleDto> CreateRuleAsync(CreateHotelPriceRuleRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        await ValidateRuleReferencesAsync(request, cancellationToken);
        var rule = CreateRule(request, _currentUser.UserId, _clock.UtcNow);
        await _repository.AddRuleAsync(rule, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);
        return ToDto(rule);
    }

    public async Task<HotelPriceRuleDto> UpdateRuleAsync(int id, UpdateHotelPriceRuleRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        var rule = await GetRequiredRuleAsync(id, cancellationToken);
        await ValidateRuleReferencesAsync(request, cancellationToken);
        if ((HotelPriceModel)request.Model == HotelPriceModel.FixedAccommodation &&
            await _repository.HasComponentsAsync(id, cancellationToken))
            throw new ValidationException("A fixed accommodation price cannot contain calculation components. Remove the components first.");
        rule.Update(request.PriceBlockId, request.HotelId, request.RoomTypeId, request.RoomCategoryId,
            request.AccommodationTypeId, request.MealTypeId, (HotelPriceModel)request.Model,
            request.Priority, request.StayDateFrom, request.StayDateTo, request.CheckInDateFrom,
            request.CheckInDateTo, request.DaysOfWeekMask, request.MinNights, request.MaxNights,
            (AgeCalculationDate)request.AgeCalculationDate, (PriceApplicationUnit)request.ApplicationUnit,
            request.NettoAmount, request.BruttoAmount, request.CurrencyId, _clock.UtcNow);
        await _repository.SaveChangesAsync(cancellationToken);
        return ToDto(rule);
    }

    public async Task DeleteRuleAsync(int id, CancellationToken cancellationToken)
    {
        _repository.RemoveRule(await GetRequiredRuleAsync(id, cancellationToken));
        await _repository.SaveChangesAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<HotelPriceComponentDto>> ListComponentsAsync(int ruleId, CancellationToken cancellationToken)
    {
        await GetRequiredRuleAsync(ruleId, cancellationToken);
        return (await _repository.ListComponentsAsync(ruleId, cancellationToken)).Select(ToDto).ToArray();
    }

    public async Task<HotelPriceComponentDto> GetComponentAsync(int id, CancellationToken cancellationToken) =>
        ToDto(await GetRequiredComponentAsync(id, cancellationToken));

    public async Task<HotelPriceComponentDto> CreateComponentAsync(
        int ruleId, CreateHotelPriceComponentRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        var rule = await GetRequiredRuleAsync(ruleId, cancellationToken);
        if (rule.Model == HotelPriceModel.FixedAccommodation)
            throw new ValidationException("Fixed accommodation price cannot contain calculation components.");
        var component = new HotelPriceComponent(ruleId, (HotelPriceComponentType)request.Type,
            request.GuestType.HasValue ? (PriceGuestType)request.GuestType.Value : null,
            (PricePlacementType)request.PlacementType, request.AgeFrom, request.AgeTo,
            (PriceOperation)request.Operation, (PriceApplicationUnit)request.ApplicationUnit,
            request.NettoAmount, request.BruttoAmount, request.Percent,
            request.PercentageBase.HasValue ? (PercentageBase)request.PercentageBase.Value : null,
            _currentUser.UserId, _clock.UtcNow);
        await _repository.AddComponentAsync(component, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);
        return ToDto(component);
    }

    public async Task<HotelPriceComponentDto> UpdateComponentAsync(
        int id, UpdateHotelPriceComponentRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        var component = await GetRequiredComponentAsync(id, cancellationToken);
        var rule = await GetRequiredRuleAsync(component.HotelPriceRuleId, cancellationToken);
        if (rule.Model == HotelPriceModel.FixedAccommodation)
            throw new ValidationException("Fixed accommodation price cannot contain calculation components.");
        component.Update((HotelPriceComponentType)request.Type,
            request.GuestType.HasValue ? (PriceGuestType)request.GuestType.Value : null,
            (PricePlacementType)request.PlacementType, request.AgeFrom, request.AgeTo,
            (PriceOperation)request.Operation, (PriceApplicationUnit)request.ApplicationUnit,
            request.NettoAmount, request.BruttoAmount, request.Percent,
            request.PercentageBase.HasValue ? (PercentageBase)request.PercentageBase.Value : null,
            _clock.UtcNow);
        await _repository.SaveChangesAsync(cancellationToken);
        return ToDto(component);
    }

    public async Task DeleteComponentAsync(int id, CancellationToken cancellationToken)
    {
        _repository.RemoveComponent(await GetRequiredComponentAsync(id, cancellationToken));
        await _repository.SaveChangesAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<HotelMealPriceRuleDto>> ListMealRulesAsync(
        int? priceBlockId, int? hotelId, CancellationToken cancellationToken) =>
        (await _repository.ListMealRulesAsync(priceBlockId, hotelId, cancellationToken)).Select(ToDto).ToArray();

    public async Task<HotelMealPriceRuleDto> GetMealRuleAsync(int id, CancellationToken cancellationToken) =>
        ToDto(await GetRequiredMealRuleAsync(id, cancellationToken));

    public async Task<HotelMealPriceRuleDto> CreateMealRuleAsync(
        CreateHotelMealPriceRuleRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        await ValidateMealReferencesAsync(request, cancellationToken);
        var rule = CreateMealRule(request, _currentUser.UserId, _clock.UtcNow);
        await _repository.AddMealRuleAsync(rule, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);
        return ToDto(rule);
    }

    public async Task<HotelMealPriceRuleDto> UpdateMealRuleAsync(
        int id, UpdateHotelMealPriceRuleRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        var rule = await GetRequiredMealRuleAsync(id, cancellationToken);
        await ValidateMealReferencesAsync(request, cancellationToken);
        rule.Update(request.PriceBlockId, request.HotelId, request.RoomTypeId,
            request.RoomCategoryId, request.AccommodationTypeId, request.FromMealTypeId,
            request.ToMealTypeId, request.StayDateFrom, request.StayDateTo,
            request.CheckInDateFrom, request.CheckInDateTo, request.DaysOfWeekMask,
            request.GuestType.HasValue ? (PriceGuestType)request.GuestType.Value : null,
            (PricePlacementType)request.PlacementType, request.AgeFrom, request.AgeTo,
            (PriceApplicationUnit)request.ApplicationUnit, (PriceOperation)request.Operation,
            request.NettoAmount, request.BruttoAmount, request.Percent,
            request.PercentageBase.HasValue ? (PercentageBase)request.PercentageBase.Value : null,
            request.CurrencyId, request.Priority, _clock.UtcNow);
        await _repository.SaveChangesAsync(cancellationToken);
        return ToDto(rule);
    }

    public async Task DeleteMealRuleAsync(int id, CancellationToken cancellationToken)
    {
        _repository.RemoveMealRule(await GetRequiredMealRuleAsync(id, cancellationToken));
        await _repository.SaveChangesAsync(cancellationToken);
    }

    private async Task ValidateRuleReferencesAsync(HotelPriceRuleWriteRequest request, CancellationToken ct)
    {
        await ValidateHotelBlockAsync(request.PriceBlockId, ct);
        if (!await _catalog.HotelExistsAsync(request.HotelId, ct)) throw ReferenceNotFound(PricingErrorMessages.HotelNotFound(request.HotelId), PricingErrorCodes.HotelNotFound);
        if (!await _catalog.RoomTypeExistsAsync(request.RoomTypeId, ct)) throw ReferenceNotFound(PricingErrorMessages.RoomTypeNotFound(request.RoomTypeId), PricingErrorCodes.RoomTypeNotFound);
        if (!await _catalog.RoomCategoryExistsAsync(request.RoomCategoryId, ct)) throw ReferenceNotFound(PricingErrorMessages.RoomCategoryNotFound(request.RoomCategoryId), PricingErrorCodes.RoomCategoryNotFound);
        if (request.AccommodationTypeId.HasValue && !await _catalog.AccommodationTypeExistsAsync(request.AccommodationTypeId.Value, ct))
            throw ReferenceNotFound(PricingErrorMessages.AccommodationTypeNotFound(request.AccommodationTypeId.Value), PricingErrorCodes.AccommodationTypeNotFound);
        if (request.MealTypeId.HasValue && !await _catalog.MealTypeExistsAsync(request.MealTypeId.Value, ct))
            throw ReferenceNotFound(PricingErrorMessages.MealTypeNotFound(request.MealTypeId.Value), PricingErrorCodes.MealTypeNotFound);
    }

    private async Task ValidateMealReferencesAsync(HotelMealPriceRuleWriteRequest request, CancellationToken ct)
    {
        await ValidateHotelBlockAsync(request.PriceBlockId, ct);
        if (!await _catalog.HotelExistsAsync(request.HotelId, ct)) throw ReferenceNotFound(PricingErrorMessages.HotelNotFound(request.HotelId), PricingErrorCodes.HotelNotFound);
        if (request.RoomTypeId.HasValue && !await _catalog.RoomTypeExistsAsync(request.RoomTypeId.Value, ct)) throw ReferenceNotFound(PricingErrorMessages.RoomTypeNotFound(request.RoomTypeId.Value), PricingErrorCodes.RoomTypeNotFound);
        if (request.RoomCategoryId.HasValue && !await _catalog.RoomCategoryExistsAsync(request.RoomCategoryId.Value, ct)) throw ReferenceNotFound(PricingErrorMessages.RoomCategoryNotFound(request.RoomCategoryId.Value), PricingErrorCodes.RoomCategoryNotFound);
        if (request.AccommodationTypeId.HasValue && !await _catalog.AccommodationTypeExistsAsync(request.AccommodationTypeId.Value, ct)) throw ReferenceNotFound(PricingErrorMessages.AccommodationTypeNotFound(request.AccommodationTypeId.Value), PricingErrorCodes.AccommodationTypeNotFound);
        if (request.FromMealTypeId.HasValue && !await _catalog.MealTypeExistsAsync(request.FromMealTypeId.Value, ct)) throw ReferenceNotFound(PricingErrorMessages.MealTypeNotFound(request.FromMealTypeId.Value), PricingErrorCodes.MealTypeNotFound);
        if (!await _catalog.MealTypeExistsAsync(request.ToMealTypeId, ct)) throw ReferenceNotFound(PricingErrorMessages.MealTypeNotFound(request.ToMealTypeId), PricingErrorCodes.MealTypeNotFound);
    }

    private async Task ValidateHotelBlockAsync(int id, CancellationToken ct)
    {
        var block = await _priceBlocks.GetByIdAsync(id, ct) ?? throw ReferenceNotFound(PricingErrorMessages.PriceBlockNotFound(id), PricingErrorCodes.PriceBlockNotFound);
        if (block.ServiceTypeId != HotelServiceTypeId) throw new ValidationException("Для гостиничной цены требуется ценовой блок с типом услуги «Отель/круиз».");
    }

    private static NotFoundException ReferenceNotFound(string message, string code) => new(message, code);

    private async Task<HotelPriceRule> GetRequiredRuleAsync(int id, CancellationToken ct) =>
        await _repository.GetRuleAsync(id, ct) ?? throw new NotFoundException($"Hotel price rule '{id}' was not found.", "hotel_price_rule_not_found");
    private async Task<HotelPriceComponent> GetRequiredComponentAsync(int id, CancellationToken ct) =>
        await _repository.GetComponentAsync(id, ct) ?? throw new NotFoundException($"Hotel price component '{id}' was not found.", "hotel_price_component_not_found");
    private async Task<HotelMealPriceRule> GetRequiredMealRuleAsync(int id, CancellationToken ct) =>
        await _repository.GetMealRuleAsync(id, ct) ?? throw new NotFoundException($"Hotel meal price rule '{id}' was not found.", "hotel_meal_price_rule_not_found");

    private static HotelPriceRule CreateRule(HotelPriceRuleWriteRequest r, string user, DateTime now) =>
        new(r.PriceBlockId, r.HotelId, r.RoomTypeId, r.RoomCategoryId, r.AccommodationTypeId,
            r.MealTypeId, (HotelPriceModel)r.Model, r.Priority, r.StayDateFrom, r.StayDateTo,
            r.CheckInDateFrom, r.CheckInDateTo, r.DaysOfWeekMask, r.MinNights, r.MaxNights,
            (AgeCalculationDate)r.AgeCalculationDate, (PriceApplicationUnit)r.ApplicationUnit,
            r.NettoAmount, r.BruttoAmount, r.CurrencyId, user, now);

    private static HotelMealPriceRule CreateMealRule(HotelMealPriceRuleWriteRequest r, string user, DateTime now) =>
        new(r.PriceBlockId, r.HotelId, r.RoomTypeId, r.RoomCategoryId, r.AccommodationTypeId,
            r.FromMealTypeId, r.ToMealTypeId, r.StayDateFrom, r.StayDateTo, r.CheckInDateFrom,
            r.CheckInDateTo, r.DaysOfWeekMask,
            r.GuestType.HasValue ? (PriceGuestType)r.GuestType.Value : null,
            (PricePlacementType)r.PlacementType, r.AgeFrom, r.AgeTo,
            (PriceApplicationUnit)r.ApplicationUnit, (PriceOperation)r.Operation,
            r.NettoAmount, r.BruttoAmount, r.Percent,
            r.PercentageBase.HasValue ? (PercentageBase)r.PercentageBase.Value : null,
            r.CurrencyId, r.Priority, user, now);

    private static HotelPriceRuleDto ToDto(HotelPriceRule x) => new()
    {
        Id=x.Id, PriceBlockId=x.PriceBlockId, HotelId=x.HotelId, RoomTypeId=x.RoomTypeId,
        RoomCategoryId=x.RoomCategoryId, AccommodationTypeId=x.AccommodationTypeId,
        MealTypeId=x.MealTypeId, Model=(byte)x.Model, Priority=x.Priority,
        StayDateFrom=x.StayDateFrom, StayDateTo=x.StayDateTo, CheckInDateFrom=x.CheckInDateFrom,
        CheckInDateTo=x.CheckInDateTo, DaysOfWeekMask=x.DaysOfWeekMask, MinNights=x.MinNights,
        MaxNights=x.MaxNights, AgeCalculationDate=(byte)x.AgeCalculationDate,
        ApplicationUnit=(byte)x.ApplicationUnit, NettoAmount=x.NettoAmount, BruttoAmount=x.BruttoAmount,
        CurrencyId=x.CurrencyId, CreatedByUserId=x.CreatedByUserId, CreatedAt=x.CreatedAt, UpdatedAt=x.UpdatedAt
    };

    private static HotelPriceComponentDto ToDto(HotelPriceComponent x) => new()
    {
        Id=x.Id, HotelPriceRuleId=x.HotelPriceRuleId, Type=(byte)x.Type,
        GuestType=x.GuestType.HasValue ? (byte)x.GuestType.Value : null,
        PlacementType=(byte)x.PlacementType, AgeFrom=x.AgeFrom, AgeTo=x.AgeTo,
        Operation=(byte)x.Operation, ApplicationUnit=(byte)x.ApplicationUnit,
        NettoAmount=x.NettoAmount, BruttoAmount=x.BruttoAmount, Percent=x.Percent,
        PercentageBase=x.PercentageBase.HasValue ? (byte)x.PercentageBase.Value : null,
        CreatedByUserId=x.CreatedByUserId, CreatedAt=x.CreatedAt, UpdatedAt=x.UpdatedAt
    };

    private static HotelMealPriceRuleDto ToDto(HotelMealPriceRule x) => new()
    {
        Id=x.Id, PriceBlockId=x.PriceBlockId, HotelId=x.HotelId, RoomTypeId=x.RoomTypeId,
        RoomCategoryId=x.RoomCategoryId, AccommodationTypeId=x.AccommodationTypeId,
        FromMealTypeId=x.FromMealTypeId, ToMealTypeId=x.ToMealTypeId,
        StayDateFrom=x.StayDateFrom, StayDateTo=x.StayDateTo, CheckInDateFrom=x.CheckInDateFrom,
        CheckInDateTo=x.CheckInDateTo, DaysOfWeekMask=x.DaysOfWeekMask,
        GuestType=x.GuestType.HasValue ? (byte)x.GuestType.Value : null,
        PlacementType=(byte)x.PlacementType, AgeFrom=x.AgeFrom, AgeTo=x.AgeTo,
        ApplicationUnit=(byte)x.ApplicationUnit, Operation=(byte)x.Operation,
        NettoAmount=x.NettoAmount, BruttoAmount=x.BruttoAmount, Percent=x.Percent,
        PercentageBase=x.PercentageBase.HasValue ? (byte)x.PercentageBase.Value : null,
        CurrencyId=x.CurrencyId, Priority=x.Priority, CreatedByUserId=x.CreatedByUserId,
        CreatedAt=x.CreatedAt, UpdatedAt=x.UpdatedAt
    };
}
