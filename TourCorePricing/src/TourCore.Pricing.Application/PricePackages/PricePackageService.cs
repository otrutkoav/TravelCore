using TourCore.Pricing.Application.Common;
using TourCore.Pricing.Application.Abstractions;
using TourCore.Pricing.Contracts.PricePackages;
using TourCore.Pricing.Domain.PricePackages;

namespace TourCore.Pricing.Application.PricePackages;

public sealed class PricePackageService
{
    private readonly IPricePackageRepository _repository;
    private readonly ICurrentUser _currentUser;
    private readonly IClock _clock;

    public PricePackageService(
        IPricePackageRepository repository,
        ICurrentUser currentUser,
        IClock clock)
    {
        _repository = repository;
        _currentUser = currentUser;
        _clock = clock;
    }

    public async Task<IReadOnlyList<PricePackageDto>> ListAsync(
        int? countryId,
        bool? isDisabled,
        string? search,
        CancellationToken cancellationToken)
    {
        var packages = await _repository.ListAsync(countryId, isDisabled, search, cancellationToken);
        return packages.Select(pricePackage => pricePackage.ToDto()).ToArray();
    }

    public async Task<PricePackageDto> GetAsync(int id, CancellationToken cancellationToken)
    {
        var pricePackage = await GetRequiredAsync(id, cancellationToken);
        return pricePackage.ToDto();
    }

    public async Task<PricePackageDto> CreateAsync(
        CreatePricePackageRequest request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        PricePackageValidator.Validate(request.Name, request.CountryId);
        await EnsureUniqueNameAsync(request.Name, request.CountryId, null, cancellationToken);

        var pricePackage = new PricePackage(
            request.Name,
            request.CountryId,
            request.IsDisabled,
            _currentUser.UserId,
            _clock.UtcNow);

        await _repository.AddAsync(pricePackage, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);
        return pricePackage.ToDto();
    }

    public async Task<PricePackageDto> UpdateAsync(
        int id,
        UpdatePricePackageRequest request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        PricePackageValidator.Validate(request.Name, request.CountryId);

        var pricePackage = await GetRequiredAsync(id, cancellationToken);
        await EnsureUniqueNameAsync(request.Name, request.CountryId, id, cancellationToken);
        pricePackage.Update(request.Name, request.CountryId, request.IsDisabled, _clock.UtcNow);
        await _repository.SaveChangesAsync(cancellationToken);
        return pricePackage.ToDto();
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var pricePackage = await GetRequiredAsync(id, cancellationToken);
        pricePackage.Delete(_clock.UtcNow);
        await _repository.SaveChangesAsync(cancellationToken);
    }

    private async Task<PricePackage> GetRequiredAsync(int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
            throw new ValidationException("Price package identifier must be greater than zero.");

        return await _repository.GetByIdAsync(id, false, cancellationToken)
            ?? throw new NotFoundException($"Price package with identifier '{id}' was not found.", "price_package_not_found");
    }

    private async Task EnsureUniqueNameAsync(
        string name,
        int countryId,
        int? excludedId,
        CancellationToken cancellationToken)
    {
        if (await _repository.NameExistsAsync(name.Trim(), countryId, excludedId, cancellationToken))
            throw new ValidationException(new Dictionary<string, string[]>
            {
                [nameof(name)] = new[] { "A price package with this name already exists for the country." }
            });
    }
}
