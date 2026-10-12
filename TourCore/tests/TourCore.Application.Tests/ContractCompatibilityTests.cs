using Microsoft.VisualStudio.TestTools.UnitTesting;
using TourCore.Contracts.Geography.Countries;
using TourCore.Contracts.Tours;
using TourCore.Search.Contracts;

namespace TourCore.Application.Tests;

[TestClass]
public sealed class ContractCompatibilityTests
{
    [TestMethod]
    public void Contract_assemblies_are_versioned_as_v1()
    {
        Assert.AreEqual(1, typeof(CountryDto).Assembly.GetName().Version?.Major);
        Assert.AreEqual(1, typeof(TourDto).Assembly.GetName().Version?.Major);
        Assert.AreEqual(1, typeof(TourSearchRequest).Assembly.GetName().Version?.Major);
    }

    [TestMethod]
    public void Contract_assemblies_do_not_reference_domain_or_application()
    {
        var assemblies = new[]
        {
            typeof(CountryDto).Assembly,
            typeof(TourDto).Assembly,
            typeof(TourSearchRequest).Assembly
        };

        foreach (var assembly in assemblies)
        {
            var forbidden = assembly.GetReferencedAssemblies()
                .Select(reference => reference.Name)
                .Where(name => name?.Contains(".Domain", StringComparison.Ordinal) == true ||
                               name?.Contains(".Application", StringComparison.Ordinal) == true)
                .ToArray();
            Assert.AreEqual(0, forbidden.Length, $"{assembly.GetName().Name}: {string.Join(", ", forbidden)}");
        }
    }

    [TestMethod]
    public void V1_core_response_shapes_remain_compatible()
    {
        AssertShape<CountryDto>(
            "CitizenshipName:String,CitizenshipNameEn:String,Code:String,DigitalCode:String,Id:Int32,IsoCode2:String,IsoCode3:String,Name:String,NameEn:String,SortOrder:Int32");
        AssertShape<TourDto>(
            "AvailableDateFrom:Nullable`1,AvailableDateTo:Nullable`1,AvailableDates:IReadOnlyCollection`1,BookingConditionsHtml:String,CreatedAt:DateTime,CurrencyId:Int32,DateMode:Int32,DescriptionHtml:String,DescriptionUrl:String,Id:Int32,Name:String,Revision:Int64,Status:Int32,TourTypes:IReadOnlyCollection`1,UpdatedAt:Nullable`1");
    }

    private static void AssertShape<T>(string expected)
    {
        var actual = string.Join(",", typeof(T).GetProperties()
            .OrderBy(property => property.Name, StringComparer.Ordinal)
            .Select(property => $"{property.Name}:{property.PropertyType.Name}"));
        Assert.AreEqual(expected, actual);
    }
}
