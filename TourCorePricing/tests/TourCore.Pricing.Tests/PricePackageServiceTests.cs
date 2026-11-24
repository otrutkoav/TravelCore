using Microsoft.VisualStudio.TestTools.UnitTesting;
using TourCore.Pricing.Application.Abstractions;
using TourCore.Pricing.Application.PricePackages;
using TourCore.Pricing.Contracts.PricePackages;
using TourCore.Pricing.Domain.PricePackages;

namespace TourCore.Pricing.Tests;

[TestClass]
public sealed class PricePackageServiceTests
{
    [TestMethod]
    public async Task Create_uses_authenticated_user_and_utc_clock()
    {
        var repository = new FakeRepository();
        var now = new DateTime(2026, 11, 23, 13, 0, 0, DateTimeKind.Utc);
        var service = new PricePackageService(repository, new FakeCurrentUser("pricing-user"), new FakeClock(now));

        var result = await service.CreateAsync(new CreatePricePackageRequest
        {
            Name = "Turkey summer",
            CountryId = 7
        }, CancellationToken.None);

        Assert.AreEqual("pricing-user", result.CreatedByUserId);
        Assert.AreEqual(now, result.CreatedAt);
        Assert.AreEqual(1, repository.SaveCount);
    }

    private sealed class FakeRepository : IPricePackageRepository
    {
        private readonly List<PricePackage> _items = new();
        public int SaveCount { get; private set; }

        public Task<IReadOnlyList<PricePackage>> ListAsync(int? countryId, bool? isDisabled, string? search, CancellationToken cancellationToken)
        {
            return Task.FromResult<IReadOnlyList<PricePackage>>(_items);
        }

        public Task<PricePackage?> GetByIdAsync(int id, bool includeDeleted, CancellationToken cancellationToken)
        {
            return Task.FromResult(_items.FirstOrDefault(item => item.Id == id));
        }

        public Task AddAsync(PricePackage pricePackage, CancellationToken cancellationToken)
        {
            _items.Add(pricePackage);
            return Task.CompletedTask;
        }

        public Task<bool> NameExistsAsync(string name, int countryId, int? excludedId, CancellationToken cancellationToken)
        {
            return Task.FromResult(false);
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            SaveCount++;
            return Task.CompletedTask;
        }
    }

    private sealed record FakeCurrentUser(string UserId) : ICurrentUser;
    private sealed record FakeClock(DateTime UtcNow) : IClock;
}
