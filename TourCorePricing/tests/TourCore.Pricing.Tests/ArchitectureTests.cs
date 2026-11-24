using Microsoft.VisualStudio.TestTools.UnitTesting;
using TourCore.Pricing.Application.PricePackages;
using TourCore.Pricing.Contracts.PricePackages;
using TourCore.Pricing.Domain.PricePackages;

namespace TourCore.Pricing.Tests;

[TestClass]
public sealed class ArchitectureTests
{
    [TestMethod]
    public void Domain_does_not_reference_application_infrastructure_or_api()
    {
        var forbidden = typeof(PricePackage).Assembly.GetReferencedAssemblies()
            .Select(reference => reference.Name)
            .Where(name => name is not null &&
                (name.Contains("Application") || name.Contains("Infrastructure") || name.EndsWith("Api")))
            .ToArray();
        CollectionAssert.AreEqual(Array.Empty<string>(), forbidden);
    }

    [TestMethod]
    public void Contracts_are_independent()
    {
        Assert.AreEqual(0, typeof(PricePackageDto).Assembly.GetReferencedAssemblies()
            .Count(reference => reference.Name?.StartsWith("TourCore.", StringComparison.Ordinal) == true));
    }

    [TestMethod]
    public void Application_does_not_reference_ef_core()
    {
        Assert.IsFalse(typeof(PricePackageService).Assembly.GetReferencedAssemblies()
            .Any(reference => reference.Name?.StartsWith("Microsoft.EntityFrameworkCore", StringComparison.Ordinal) == true));
    }
}
