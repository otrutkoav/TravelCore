using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TourCore.Application.Abstractions.Caching;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Abstractions.Persistence.Geography;
using TourCore.Application.Abstractions.Services;
using TourCore.Application.Common.Errors;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Geography.Countries.Commands;
using TourCore.Application.Geography.Countries.Handlers;
using TourCore.Application.Geography.Countries.Validators;
using TourCore.Domain.Geography.Entities;

namespace TourCore.Catalog.UnitTests;

[TestClass]
public sealed class CreateCountryHandlerTests
{
    private static readonly DateTime UtcNow = new(2026, 10, 12, 9, 30, 0, DateTimeKind.Utc);

    [TestMethod]
    public async Task Handle_persists_country_and_notifies_catalog_change()
    {
        var repository = CreateRepositoryWithoutDuplicates();
        Country? addedCountry = null;
        repository
            .Setup(x => x.AddAsync(It.IsAny<Country>(), It.IsAny<CancellationToken>()))
            .Callback<Country, CancellationToken>((country, _) => addedCountry = country)
            .Returns(Task.CompletedTask);

        var unitOfWork = new Mock<IUnitOfWork>();
        unitOfWork.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);
        var cacheUpdater = new Mock<ICatalogCacheUpdater>();
        cacheUpdater
            .Setup(x => x.CatalogChangedAsync(CatalogCacheKeys.Countries, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
        var clock = new Mock<IDateTimeProvider>();
        clock.SetupGet(x => x.UtcNow).Returns(UtcNow);

        var handler = new CreateCountryHandler(
            repository.Object,
            unitOfWork.Object,
            clock.Object,
            new CreateCountryCommandValidator(),
            cacheUpdater.Object);

        var result = await handler.Handle(ValidCommand(), CancellationToken.None);

        Assert.IsNotNull(addedCountry);
        Assert.AreEqual("RU", addedCountry.Code);
        Assert.AreEqual(UtcNow, addedCountry.CreatedAt);
        Assert.AreEqual("RU", result.Code);
        unitOfWork.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        cacheUpdater.Verify(
            x => x.CatalogChangedAsync(CatalogCacheKeys.Countries, It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [TestMethod]
    public async Task Handle_rejects_duplicate_code_without_saving()
    {
        var repository = CreateRepositoryWithoutDuplicates();
        repository
            .Setup(x => x.ExistsByCodeAsync("RU", It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);
        var unitOfWork = new Mock<IUnitOfWork>();
        var cacheUpdater = new Mock<ICatalogCacheUpdater>();
        var clock = new Mock<IDateTimeProvider>();
        clock.SetupGet(x => x.UtcNow).Returns(UtcNow);
        var handler = new CreateCountryHandler(
            repository.Object,
            unitOfWork.Object,
            clock.Object,
            new CreateCountryCommandValidator(),
            cacheUpdater.Object);

        var exception = await Assert.ThrowsExceptionAsync<ConflictException>(() =>
            handler.Handle(ValidCommand(), CancellationToken.None));

        Assert.AreEqual(ErrorCode.CountryCodeExists, exception.Code);
        unitOfWork.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        cacheUpdater.Verify(
            x => x.CatalogChangedAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()),
            Times.Never);
    }

    private static Mock<ICountryRepository> CreateRepositoryWithoutDuplicates()
    {
        var repository = new Mock<ICountryRepository>();
        repository.Setup(x => x.ExistsByCodeAsync(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(false);
        repository.Setup(x => x.ExistsByIsoCode2Async(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(false);
        repository.Setup(x => x.ExistsByIsoCode3Async(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(false);
        return repository;
    }

    private static CreateCountryCommand ValidCommand() => new()
    {
        Name = "Россия",
        NameEn = "Russia",
        Code = " ru ",
        IsoCode2 = "ru",
        IsoCode3 = "rus",
        DigitalCode = "643",
        SortOrder = 10
    };
}
