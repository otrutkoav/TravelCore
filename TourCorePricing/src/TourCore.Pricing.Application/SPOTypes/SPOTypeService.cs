using TourCore.Pricing.Application.Abstractions;
using TourCore.Pricing.Application.Common;
using TourCore.Pricing.Contracts.SPOTypes;
using TourCore.Pricing.Domain.SPOTypes;

namespace TourCore.Pricing.Application.SPOTypes;

public sealed class SPOTypeService
{
    private readonly ISPOTypeRepository _repository;
    private readonly ICurrentUser _currentUser;
    private readonly IClock _clock;

    public SPOTypeService(ISPOTypeRepository repository, ICurrentUser currentUser, IClock clock)
    {
        _repository = repository;
        _currentUser = currentUser;
        _clock = clock;
    }

    public async Task<IReadOnlyList<SPOTypeDto>> ListAsync(CancellationToken cancellationToken)
    {
        var items = await _repository.ListAsync(cancellationToken);
        return items.Select(ToDto).ToArray();
    }

    public async Task<SPOTypeDto> GetAsync(int id, CancellationToken cancellationToken)
    {
        return ToDto(await GetRequiredAsync(id, cancellationToken));
    }

    public async Task<SPOTypeDto> CreateAsync(
        CreateSPOTypeRequest request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        Validate(request.Name, request.NameLat);

        await EnsureUniqueNameAsync(request.Name, null, cancellationToken);
        var spoType = new SPOType(request.Name, request.NameLat, _currentUser.UserId, _clock.UtcNow);
        await _repository.AddAsync(spoType, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);
        return ToDto(spoType);
    }

    public async Task<SPOTypeDto> UpdateAsync(
        int id,
        UpdateSPOTypeRequest request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        Validate(request.Name, request.NameLat);

        var spoType = await GetRequiredAsync(id, cancellationToken);
        await EnsureUniqueNameAsync(request.Name, id, cancellationToken);
        spoType.Update(request.Name, request.NameLat, _clock.UtcNow);
        await _repository.SaveChangesAsync(cancellationToken);
        return ToDto(spoType);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var spoType = await GetRequiredAsync(id, cancellationToken);
        _repository.Remove(spoType);
        await _repository.SaveChangesAsync(cancellationToken);
    }

    private async Task<SPOType> GetRequiredAsync(int id, CancellationToken cancellationToken)
    {
        if (id < 0)
            throw new ValidationException("SPO type identifier cannot be negative.");

        return await _repository.GetByIdAsync(id, cancellationToken)
            ?? throw new NotFoundException($"SPO type with identifier '{id}' was not found.", "spo_type_not_found");
    }

    private async Task EnsureUniqueNameAsync(
        string name,
        int? excludedId,
        CancellationToken cancellationToken)
    {
        if (await _repository.NameExistsAsync(name.Trim(), excludedId, cancellationToken))
            throw new ValidationException(new Dictionary<string, string[]>
            {
                [nameof(name)] = new[] { "SPO type name already exists." }
            });
    }

    private static void Validate(string? name, string? nameLat)
    {
        var errors = new Dictionary<string, string[]>();
        ValidateName(name, nameof(name), errors);
        ValidateName(nameLat, nameof(nameLat), errors);
        if (errors.Count > 0)
            throw new ValidationException(errors);
    }

    private static void ValidateName(
        string? value,
        string propertyName,
        IDictionary<string, string[]> errors)
    {
        if (string.IsNullOrWhiteSpace(value))
            errors[propertyName] = new[] { $"{propertyName} is required." };
        else if (value.Trim().Length > SPOType.MaximumNameLength)
            errors[propertyName] = new[] { $"{propertyName} must be {SPOType.MaximumNameLength} characters or less." };
    }

    private static SPOTypeDto ToDto(SPOType spoType)
    {
        return new SPOTypeDto
        {
            Id = spoType.Id,
            Name = spoType.Name,
            NameLat = spoType.NameLat,
            CreatedByUserId = spoType.CreatedByUserId,
            CreatedAt = spoType.CreatedAt,
            UpdatedAt = spoType.UpdatedAt
        };
    }
}
