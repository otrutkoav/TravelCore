public static class ErrorCode
{
    // Общие
    public const string ValidationError = "validation_error";
    public const string NotFound = "not_found";
    public const string Conflict = "conflict";
    public const string DomainError = "domain_error";
    public const string InternalError = "internal_error";

    // Country
    public const string CountryNotFound = "country_not_found";
    public const string CountryCodeExists = "country_code_exists";
    public const string CountryIsoCode2Exists = "country_iso_code2_exists";
    public const string CountryIsoCode3Exists = "country_iso_code3_exists";

    // Validation (общие для всех сущностей)
    public const string Required = "required";
    public const string MaxLength = "max_length";
    public const string ExactLength = "exact_length";
    public const string GreaterThanZero = "greater_than_zero";
    public const string Negative = "negative";
}