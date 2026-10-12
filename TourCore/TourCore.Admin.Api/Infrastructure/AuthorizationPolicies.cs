namespace TourCore.Admin.Api.Infrastructure;

public static class AuthorizationPolicies
{
    public const string CatalogRead = "Catalog.Read";
    public const string CatalogWrite = "Catalog.Write";
    public const string CatalogDelete = "Catalog.Delete";
    public const string CatalogFinanceWrite = "Catalog.Finance.Write";
}

public static class AuthorizationRoles
{
    public const string CatalogReader = "CatalogReader";
    public const string CatalogEditor = "CatalogEditor";
    public const string CatalogAdmin = "CatalogAdmin";
}
