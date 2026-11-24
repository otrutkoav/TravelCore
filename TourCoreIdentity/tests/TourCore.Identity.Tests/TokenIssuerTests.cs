using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TourCore.Identity.Api.Tests;

[TestClass]
public sealed class TokenIssuerTests
{
    [TestMethod]
    public void Issue_IncludesAllConfiguredAudiencesAndRoles()
    {
        var options = Options.Create(new JwtIssuerOptions
        {
            Issuer = "TourCore.Tests",
            Audiences = ["TourCore.Admin.Api", "TourCore.Pricing.Api"],
            SigningKey = "TourCore.Identity.Tests.SigningKey.AtLeast32Chars",
            AccessTokenMinutes = 5
        });
        var issuer = new JwtAccessTokenIssuer(options);

        var response = issuer.Issue("admin", ["CatalogAdmin", "PricingAdmin"]);
        var token = new JwtSecurityTokenHandler().ReadJwtToken(response.AccessToken);

        CollectionAssert.AreEquivalent(
            new[] { "TourCore.Admin.Api", "TourCore.Pricing.Api" },
            token.Audiences.ToArray());
        CollectionAssert.AreEquivalent(
            new[] { "CatalogAdmin", "PricingAdmin" },
            token.Claims.Where(claim => claim.Type == "role").Select(claim => claim.Value).ToArray());
        Assert.AreEqual("admin", token.Subject);
    }

    [TestMethod]
    public void PasswordVerifier_AcceptsDevelopmentPasswordAndRejectsAnotherPassword()
    {
        const string hash = "pbkdf2-sha256$210000$VG91ckNvcmUuRGV2LkFkbWluLlNhbHQuMjAyNg==$9+NHvXQS5v0cS2a4mz6Ur3U1lFPM9RmCo9xDY7uZmU8=";
        var verifier = new Pbkdf2PasswordVerifier();

        Assert.IsTrue(verifier.Verify("TourCore.Admin.2026!", hash));
        Assert.IsFalse(verifier.Verify("wrong-password", hash));
    }
}
