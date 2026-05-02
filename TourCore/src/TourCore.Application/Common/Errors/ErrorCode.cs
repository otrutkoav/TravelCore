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

    // RoomType
    public const string RoomTypeNotFound = "room_type_not_found";
    public const string RoomTypeCodeExists = "room_type_code_exists";

    // HotelCategory
    public const string HotelCategoryNotFound = "hotel_category_not_found";
    public const string HotelCategoryNameExists = "hotel_category_name_exists";
    public const string HotelCategoryGlobalCodeExists = "hotel_category_global_code_exists";

    // Rate
    public const string RateNotFound = "rate_not_found";
    public const string RateCodeExists = "rate_code_exists";
    public const string RateIsoCodeExists = "rate_iso_code_exists";

    // RealCourse
    public const string RealCourseNotFound = "real_course_not_found";
    public const string RealCourseExists = "real_course_exists";
    public const string FromRateNotFound = "from_rate_not_found";
    public const string ToRateNotFound = "to_rate_not_found";
    public const string SameRateCodes = "same_rate_codes";
    public const string DateRangeInvalid = "date_range_invalid";

    // AirClass
    public const string AirClassNotFound = "air_class_not_found";
    public const string AirClassCodeExists = "air_class_code_exists";

    // Aircraft
    public const string AircraftNotFound = "aircraft_not_found";
    public const string AircraftCodeExists = "aircraft_code_exists";

    // Airline
    public const string AirlineNotFound = "airline_not_found";
    public const string AirlineCodeExists = "airline_code_exists";
    public const string AirlineIcaoCodeExists = "airline_icao_code_exists";

    // Airport
    public const string AirportNotFound = "airport_not_found";
    public const string AirportCodeExists = "airport_code_exists";
    public const string AirportIcaoCodeExists = "airport_icao_code_exists";

    // Charter
    public const string CharterNotFound = "charter_not_found";
    public const string CharterExists = "charter_exists";
    public const string DepartureCityNotFound = "departure_city_not_found";
    public const string ArrivalCityNotFound = "arrival_city_not_found";
    public const string DepartureAirportNotFound = "departure_airport_not_found";
    public const string ArrivalAirportNotFound = "arrival_airport_not_found";
}