using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TourCore.Application.Tests;

[TestClass]
public sealed class ProjectSmokeTests
{
    [TestMethod]
    public void Test_project_runs_on_modern_dotnet()
    {
        Assert.IsTrue(Environment.Version.Major >= 8);
    }
}
