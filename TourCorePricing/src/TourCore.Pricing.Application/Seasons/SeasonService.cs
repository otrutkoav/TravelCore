using TourCore.Pricing.Application.Abstractions;
using TourCore.Pricing.Application.Common;
using TourCore.Pricing.Contracts.Seasons;
using TourCore.Pricing.Domain.Seasons;

namespace TourCore.Pricing.Application.Seasons;

public sealed class SeasonService
{
    private readonly ISeasonRepository _repository;
    private readonly ICurrentUser _currentUser;
    private readonly IClock _clock;

    public SeasonService(ISeasonRepository repository, ICurrentUser currentUser, IClock clock)
    {
        _repository = repository;
        _currentUser = currentUser;
        _clock = clock;
    }

    public async Task<IReadOnlyList<SeasonDto>> ListAsync(bool? isActive, CancellationToken cancellationToken)
    {
        return (await _repository.ListAsync(isActive, cancellationToken)).Select(ToDto).ToArray();
    }

    public async Task<SeasonDto> GetAsync(int id, CancellationToken cancellationToken)
    {
        return ToDto(await GetRequiredAsync(id, cancellationToken));
    }

    public async Task<SeasonDto> CreateAsync(CreateSeasonRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        await ValidateUniqueAsync(request.Name, request.IsMain, null, cancellationToken);
        var season = new Season(request.Name, request.NameLat, request.IsActive, request.IsMain,
            _currentUser.UserId, _clock.UtcNow);
        await _repository.AddAsync(season, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);
        return ToDto(season);
    }

    public async Task<SeasonDto> UpdateAsync(int id, UpdateSeasonRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        var season = await GetRequiredAsync(id, cancellationToken);
        await ValidateUniqueAsync(request.Name, request.IsMain, id, cancellationToken);
        season.Update(request.Name, request.NameLat, request.IsActive, request.IsMain, _clock.UtcNow);
        await _repository.SaveChangesAsync(cancellationToken);
        return ToDto(season);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var season = await GetRequiredAsync(id, cancellationToken);
        _repository.Remove(season);
        await _repository.SaveChangesAsync(cancellationToken);
    }

    private async Task ValidateUniqueAsync(string name, bool isMain, int? excludedId, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ValidationException("Season name is required.");
        if (await _repository.NameExistsAsync(name.Trim(), excludedId, cancellationToken))
            throw new ValidationException("Season name already exists.");
        if (isMain && await _repository.MainExistsAsync(excludedId, cancellationToken))
            throw new ValidationException("Only one main season is allowed.");
    }

    private async Task<Season> GetRequiredAsync(int id, CancellationToken cancellationToken)
    {
        if (id < 0)
            throw new ValidationException("Season identifier cannot be negative.");
        return await _repository.GetByIdAsync(id, cancellationToken)
            ?? throw new NotFoundException($"Season with identifier '{id}' was not found.", "season_not_found");
    }

    private static SeasonDto ToDto(Season season) => new()
    {
        Id = season.Id, Name = season.Name, NameLat = season.NameLat,
        IsActive = season.IsActive, IsMain = season.IsMain,
        CreatedByUserId = season.CreatedByUserId,
        CreatedAt = season.CreatedAt, UpdatedAt = season.UpdatedAt
    };
}
