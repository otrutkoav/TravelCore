using TourCore.Pricing.Application.Abstractions;
using TourCore.Pricing.Application.Common;
using TourCore.Pricing.Contracts.PriceBlocks;
using TourCore.Pricing.Domain.PriceBlocks;

namespace TourCore.Pricing.Application.PriceBlocks;

public sealed class PriceBlockService
{
    private readonly IPriceBlockRepository _repository;
    private readonly IPricePackageRepository _pricePackageRepository;
    private readonly ISPOTypeRepository _spoTypeRepository;
    private readonly ICurrentUser _currentUser;
    private readonly IClock _clock;

    public PriceBlockService(
        IPriceBlockRepository repository,
        IPricePackageRepository pricePackageRepository,
        ISPOTypeRepository spoTypeRepository,
        ICurrentUser currentUser,
        IClock clock)
    {
        _repository = repository;
        _pricePackageRepository = pricePackageRepository;
        _spoTypeRepository = spoTypeRepository;
        _currentUser = currentUser;
        _clock = clock;
    }

    public async Task<IReadOnlyList<PriceBlockDto>> ListAsync(
        int? pricePackageId,
        CancellationToken cancellationToken)
    {
        var items = await _repository.ListAsync(pricePackageId, cancellationToken);
        return items.Select(ToDto).ToArray();
    }

    public async Task<PriceBlockDto> GetAsync(int id, CancellationToken cancellationToken)
    {
        return ToDto(await GetRequiredAsync(id, cancellationToken));
    }

    public async Task<PriceBlockDto> CreateAsync(
        CreatePriceBlockRequest request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        await ValidateReferencesAsync(request.PricePackageId, request.SPOTypeId, cancellationToken);
        var now = _clock.UtcNow;
        var priceBlock = new PriceBlock(
            request.PricePackageId, request.SPOTypeId, request.SeasonId, request.ServiceTypeId,
            request.PartnerId, request.CurrencyId, request.Name, request.Code, request.Description,
            request.Comment, request.OnlineInfoText, request.SaleStartsAt, request.SaleEndsAt,
            request.DaysBeforeCheckInFrom, request.DaysBeforeCheckInTo,
            request.ExcludeFromRecalculation, _currentUser.UserId, now);
        await _repository.AddAsync(priceBlock, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);
        return ToDto(priceBlock);
    }

    public async Task<PriceBlockDto> UpdateAsync(
        int id,
        UpdatePriceBlockRequest request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        var priceBlock = await GetRequiredAsync(id, cancellationToken);
        await ValidateReferencesAsync(request.PricePackageId, request.SPOTypeId, cancellationToken);
        priceBlock.Update(
            request.PricePackageId, request.SPOTypeId, request.SeasonId, request.ServiceTypeId,
            request.PartnerId, request.CurrencyId, request.Name, request.Code, request.Description,
            request.Comment, request.OnlineInfoText, request.SaleStartsAt, request.SaleEndsAt,
            request.DaysBeforeCheckInFrom, request.DaysBeforeCheckInTo,
            request.ExcludeFromRecalculation, _clock.UtcNow);
        await _repository.SaveChangesAsync(cancellationToken);
        return ToDto(priceBlock);
    }

    public async Task<PriceBlockDto> ChangeStateAsync(
        int id,
        ChangePriceBlockStateRequest request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        if (!Enum.IsDefined(typeof(CostOfferState), request.State))
            throw new ValidationException("Unknown price block state.");
        var priceBlock = await GetRequiredAsync(id, cancellationToken);
        priceBlock.ChangeState((CostOfferState)request.State, _currentUser.UserId, _clock.UtcNow);
        await _repository.SaveChangesAsync(cancellationToken);
        return ToDto(priceBlock);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var priceBlock = await GetRequiredAsync(id, cancellationToken);
        _repository.Remove(priceBlock);
        await _repository.SaveChangesAsync(cancellationToken);
    }

    private async Task ValidateReferencesAsync(
        int pricePackageId,
        int spoTypeId,
        CancellationToken cancellationToken)
    {
        if (await _pricePackageRepository.GetByIdAsync(pricePackageId, false, cancellationToken) is null)
            throw new ValidationException("Price package does not exist or is deleted.");
        if (await _spoTypeRepository.GetByIdAsync(spoTypeId, cancellationToken) is null)
            throw new ValidationException("SPO type does not exist.");
    }

    private async Task<PriceBlock> GetRequiredAsync(int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
            throw new ValidationException("Price block identifier must be greater than zero.");
        return await _repository.GetByIdAsync(id, cancellationToken)
            ?? throw new NotFoundException($"Price block with identifier '{id}' was not found.", "price_block_not_found");
    }

    private static PriceBlockDto ToDto(PriceBlock item)
    {
        return new PriceBlockDto
        {
            Id = item.Id, PricePackageId = item.PricePackageId, SPOTypeId = item.SPOTypeId,
            SeasonId = item.SeasonId, ServiceTypeId = item.ServiceTypeId, PartnerId = item.PartnerId,
            CurrencyId = item.CurrencyId, Name = item.Name, Code = item.Code,
            Description = item.Description, Comment = item.Comment, OnlineInfoText = item.OnlineInfoText,
            SaleStartsAt = item.SaleStartsAt, SaleEndsAt = item.SaleEndsAt,
            DaysBeforeCheckInFrom = item.DaysBeforeCheckInFrom,
            DaysBeforeCheckInTo = item.DaysBeforeCheckInTo,
            ExcludeFromRecalculation = item.ExcludeFromRecalculation, State = (byte)item.State,
            CreatedAt = item.CreatedAt, UpdatedAt = item.UpdatedAt,
            SentForReviewAt = item.SentForReviewAt, ActivatedForNetAt = item.ActivatedForNetAt,
            ActivatedAt = item.ActivatedAt, DeactivatedAt = item.DeactivatedAt,
            CreatedByUserId = item.CreatedByUserId,
            SentForReviewByUserId = item.SentForReviewByUserId,
            ActivatedForNetByUserId = item.ActivatedForNetByUserId,
            ActivatedByUserId = item.ActivatedByUserId,
            DeactivatedByUserId = item.DeactivatedByUserId
        };
    }
}
