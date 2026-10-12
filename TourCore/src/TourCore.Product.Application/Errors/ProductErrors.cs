namespace TourCore.Product.Application.Errors;

public static class ProductErrorCode
{
    public const string CurrencyNotFound = "rate_not_found";
    public const string ServiceTypeNotFound = "service_type_not_found";
    public const string TourNotFound = "tour_not_found";
    public const string TourTypeNotFound = "tour_type_not_found";
    public const string TourTypeParentNotFound = "tour_type_parent_not_found";
    public const string TourTypeNameExists = "tour_type_name_exists";
    public const string TourTypeCycle = "tour_type_cycle";
    public const string TourTypeHasChildren = "tour_type_has_children";
    public const string TourTypeIsUsed = "tour_type_is_used";
    public const string TourServiceNotFound = "tour_service_not_found";
    public const string TourServiceParentNotFound = "tour_service_parent_not_found";
    public const string TourServiceHasChildren = "tour_service_has_children";
}

public static class ProductErrorMessages
{
    public const string CurrencyNotFound = "Валюта не найдена.";
    public const string ServiceTypeNotFound = "Тип услуги не найден.";
    public const string TourNotFound = "Тур не найден.";
    public const string TourTypeNotFound = "Тип тура не найден.";
    public const string TourTypeParentNotFound = "Родительский тип тура не найден.";
    public const string TourTypeNameExists = "Тип тура с таким названием уже существует.";
    public const string TourTypeCycle = "Нельзя создать циклическую иерархию типов туров.";
    public const string TourTypeHasChildren = "Нельзя удалить тип тура, у которого есть дочерние типы.";
    public const string TourTypeIsUsed = "Нельзя удалить тип тура, используемый в турах.";
    public const string TourServiceNotFound = "Услуга тура не найдена.";
    public const string TourServiceParentNotFound = "Родительская услуга тура не найдена.";
    public const string TourServiceHasChildren = "Нельзя удалить услугу тура, у которой есть дочерние услуги.";
}
