namespace TourCore.Pricing.Application.Common;

/// <summary>Стабильные коды ошибок публичного Pricing API.</summary>
public static class PricingErrorCodes
{
    public const string PriceBlockNotFound = "price_block_not_found";
    public const string HotelNotFound = "hotel_not_found";
    public const string RoomTypeNotFound = "room_type_not_found";
    public const string RoomCategoryNotFound = "room_category_not_found";
    public const string AccommodationTypeNotFound = "accommodation_type_not_found";
    public const string MealTypeNotFound = "meal_type_not_found";
    public const string CharterNotFound = "charter_not_found";
    public const string AirClassNotFound = "air_class_not_found";
    public const string BusTransferPointNotFound = "bus_transfer_point_not_found";
    public const string RailwayTransferNotFound = "railway_transfer_not_found";
    public const string TransportNotFound = "transport_not_found";
}

/// <summary>Сообщения об отсутствующих связанных объектах.</summary>
public static class PricingErrorMessages
{
    public static string PriceBlockNotFound(int id) => $"Ценовой блок с идентификатором '{id}' не найден.";
    public static string HotelNotFound(int id) => $"Отель с идентификатором '{id}' не найден.";
    public static string RoomTypeNotFound(int id) => $"Тип номера с идентификатором '{id}' не найден.";
    public static string RoomCategoryNotFound(int id) => $"Категория номера с идентификатором '{id}' не найдена.";
    public static string AccommodationTypeNotFound(int id) => $"Тип размещения с идентификатором '{id}' не найден.";
    public static string MealTypeNotFound(int id) => $"Тип питания с идентификатором '{id}' не найден.";
    public static string CharterNotFound(int id) => $"Авиаперелёт с идентификатором '{id}' не найден.";
    public static string AirClassNotFound(int id) => $"Класс обслуживания с идентификатором '{id}' не найден.";
    public static string BusTransferPointNotFound(int id) => $"Точка автобусного переезда с идентификатором '{id}' не найдена.";
    public static string RailwayTransferNotFound(int id) => $"ЖД-переезд с идентификатором '{id}' не найден.";
    public static string TransportNotFound(int id) => $"Транспорт с идентификатором '{id}' не найден.";
}
