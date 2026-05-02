namespace TourCore.Application.Common.Errors
{
    public static class ErrorMessages
    {
        public const string ValidationError = "Ошибка валидации.";
        public const string InternalError = "Внутренняя ошибка сервера.";

        // Страна
        public const string CountryNotFound = "Страна не найдена.";
        public const string CountryCodeExists = "Страна с таким кодом уже существует.";
        public const string CountryIsoCode2Exists = "Страна с таким ISO2 кодом уже существует.";
        public const string CountryIsoCode3Exists = "Страна с таким ISO3 кодом уже существует.";

        // Город
        public const string CityNotFound = "Город не найден.";
        public const string CityCodeExists = "Город с таким кодом уже существует.";
        public const string RegionNotFound = "Регион не найден.";
        public const string RegionCountryMismatch = "Регион не принадлежит выбранной стране.";

        // Регион
        public const string RegionCodeExists = "Регион с таким кодом уже существует.";

        // Курорт
        public const string ResortNotFound = "Курорт не найден.";
        public const string ResortNameExists = "Курорт с таким названием уже существует.";

        // Тип питания
        public const string MealTypeNotFound = "Тип питания не найден.";
        public const string MealTypeCodeExists = "Тип питания с таким кодом уже существует.";
        public const string MealTypeGlobalCodeExists = "Тип питания с таким глобальным кодом уже существует.";

        // Категория номера
        public const string RoomCategoryNotFound = "Категория номера не найдена.";
        public const string RoomCategoryCodeExists = "Категория номера с таким кодом уже существует.";

        // Тип номера
        public const string RoomTypeNotFound = "Тип номера не найден.";
        public const string RoomTypeCodeExists = "Тип номера с таким кодом уже существует.";

        // Категория отеля
        public const string HotelCategoryNotFound = "Категория отеля не найдена.";
        public const string HotelCategoryNameExists = "Категория отеля с таким названием уже существует.";
        public const string HotelCategoryGlobalCodeExists = "Категория отеля с таким глобальным кодом уже существует.";

        // Валюта
        public const string RateNotFound = "Валюта не найдена.";
        public const string RateCodeExists = "Валюта с таким кодом уже существует.";
        public const string RateIsoCodeExists = "Валюта с таким ISO кодом уже существует.";

        // Реальный курс
        public const string RealCourseNotFound = "Курс валют не найден.";
        public const string RealCourseExists = "Курс для выбранной пары валют и периода уже существует.";
        public const string FromRateNotFound = "Исходная валюта не найдена.";
        public const string ToRateNotFound = "Целевая валюта не найдена.";

        // Класс обслуживания
        public const string AirClassNotFound = "Класс обслуживания не найден.";
        public const string AirClassCodeExists = "Класс обслуживания с таким кодом уже существует.";

        // Воздушное судно
        public const string AircraftNotFound = "Воздушное судно не найдено.";
        public const string AircraftCodeExists = "Воздушное судно с таким кодом уже существует.";

        // Авиакомпания
        public const string AirlineNotFound = "Авиакомпания не найдена.";
        public const string AirlineCodeExists = "Авиакомпания с таким кодом уже существует.";
        public const string AirlineIcaoCodeExists = "Авиакомпания с таким ICAO кодом уже существует.";

        // Аэропорт
        public const string AirportNotFound = "Аэропорт не найден.";
        public const string AirportCodeExists = "Аэропорт с таким кодом уже существует.";
        public const string AirportIcaoCodeExists = "Аэропорт с таким ICAO кодом уже существует.";

        // Чартер
        public const string CharterNotFound = "Чартер не найден.";
        public const string CharterExists = "Такой чартер уже существует.";
        public const string DepartureCityNotFound = "Город вылета не найден.";
        public const string ArrivalCityNotFound = "Город прилета не найден.";
        public const string DepartureAirportNotFound = "Аэропорт вылета не найден.";
        public const string ArrivalAirportNotFound = "Аэропорт прилета не найден.";
    }
}