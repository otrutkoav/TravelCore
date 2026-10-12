using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TourCore.Application.Tests;

[TestClass]
public sealed class ProjectSmokeTests
{
    [TestMethod]
    public void Test_project_runs_on_net_10()
    {
        Assert.IsTrue(Environment.Version.Major >= 10);
    }
}
