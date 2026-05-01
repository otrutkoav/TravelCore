public static class ErrorCode
{
    // Общие
    public const string ValidationError = "validation_error";
    public const string NotFound = "not_found";
    public const string Conflict = "conflict";
    public const string DomainError = "domain_error";
    public const string InternalError = "internal_error";

    // Validation (общие для всех сущностей)
    public const string Required = "required";
    public const string MaxLength = "max_length";
    public const string ExactLength = "exact_length";
    public const string GreaterThanZero = "greater_than_zero";
    public const string Negative = "negative";

    // Country
    public const string CountryNotFound = "country_not_found";
    public const string CountryCodeExists = "country_code_exists";
    public const string CountryIsoCode2Exists = "country_iso_code2_exists";
    public const string CountryIsoCode3Exists = "country_iso_code3_exists";

    // City
    public const string CityNotFound = "city_not_found";
    public const string CityCodeExists = "city_code_exists";
    public const string RegionNotFound = "region_not_found";
    public const string RegionCountryMismatch = "region_country_mismatch";

    // Regio
    public const string RegionCodeExists = "region_code_exists";

    // Resort
    public const string ResortNotFound = "resort_not_found";
    public const string ResortNameExists = "resort_name_exists";

    // MealType
    public const string MealTypeNotFound = "meal_type_not_found";
    public const string MealTypeCodeExists = "meal_type_code_exists";
    public const string MealTypeGlobalCodeExists = "meal_type_global_code_exists";

    // RoomCategory
    public const string RoomCategoryNotFound = "room_category_not_found";
    public const string RoomCategoryCodeExists = "room_category_code_exists";
}