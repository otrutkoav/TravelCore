using Microsoft.VisualStudio.TestTools.UnitTesting;
using TourCore.Domain.Geography.Entities;
using TourCore.Domain.Tours;

namespace TourCore.Application.Tests;

[TestClass]
public sealed class ModuleBoundaryTests
{
    [TestMethod]
    public void Catalog_and_product_domain_are_separate_assemblies()
    {
        Assert.AreEqual("TourCore.Catalog.Domain", typeof(Country).Assembly.GetName().Name);
        Assert.AreEqual("TourCore.Product.Domain", typeof(Tour).Assembly.GetName().Name);
        Assert.AreNotSame(typeof(Country).Assembly, typeof(Tour).Assembly);
    }

    [TestMethod]
    public void Catalog_domain_does_not_contain_product_types()
    {
        var productTypes = typeof(Country).Assembly
            .GetTypes()
            .Where(type => type.Namespace?.StartsWith("TourCore.Domain.Tours", StringComparison.Ordinal) == true)
            .ToArray();

        Assert.AreEqual(0, productTypes.Length);
    }

    [TestMethod]
    public void Product_domain_does_not_reference_catalog_assemblies()
    {
        var references = typeof(Tour).Assembly.GetReferencedAssemblies()
            .Select(reference => reference.Name)
            .Where(name => name?.StartsWith("TourCore.Catalog.", StringComparison.Ordinal) == true)
            .ToArray();

        Assert.AreEqual(0, references.Length);
    }

    [TestMethod]
    public void Product_application_does_not_reference_catalog_implementation_or_domain()
    {
        var references = typeof(TourCore.Application.Tours.CreateTourHandler).Assembly
            .GetReferencedAssemblies()
            .Select(reference => reference.Name)
            .Where(name => name is "TourCore.Catalog.Application" or "TourCore.Catalog.Domain")
            .ToArray();

        Assert.AreEqual(0, references.Length);
    }
}
