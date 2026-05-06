namespace TourCore.Application.Common.Errors
{ 
    public static class ErrorMessages
    {
        // ============================================================
        // Общие сообщения (используются во всем API)
        // ============================================================
        public const string ValidationError = "Ошибка валидации.";
        public const string InternalError = "Внутренняя ошибка сервера.";

        // ============================================================
        // Сообщения по сущностям (business errors)
        // Используются в handlers при выбрасывании исключений
        // ============================================================

        // Country
        public const string CountryNotFound = "Страна не найдена.";
        public const string CountryCodeExists = "Страна с таким кодом уже существует.";
        public const string CountryIsoCode2Exists = "Страна с таким ISO2 кодом уже существует.";
        public const string CountryIsoCode3Exists = "Страна с таким ISO3 кодом уже существует.";

        // City
        public const string CityNotFound = "Город не найден.";
        public const string CityCodeExists = "Город с таким кодом уже существует.";
        public const string RegionNotFound = "Регион не найден.";
        public const string RegionCountryMismatch = "Регион не принадлежит выбранной стране.";

        // Region
        public const string RegionCodeExists = "Регион с таким кодом уже существует.";

        // Resort
        public const string ResortNotFound = "Курорт не найден.";
        public const string ResortNameExists = "Курорт с таким названием уже существует.";

        // Отель
        public const string HotelNotFound = "Отель не найден.";
        public const string HotelCodeExists = "Отель с таким кодом уже существует.";

        // MealType
        public const string MealTypeNotFound = "Тип питания не найден.";
        public const string MealTypeCodeExists = "Тип питания с таким кодом уже существует.";
        public const string MealTypeGlobalCodeExists = "Тип питания с таким глобальным кодом уже существует.";

        // RoomCategory
        public const string RoomCategoryNotFound = "Категория номера не найдена.";
        public const string RoomCategoryCodeExists = "Категория номера с таким кодом уже существует.";

        // RoomType
        public const string RoomTypeNotFound = "Тип номера не найден.";
        public const string RoomTypeCodeExists = "Тип номера с таким кодом уже существует.";

        // Тип размещения
        public const string AccommodationTypeNotFound = "Тип размещения не найден.";
        public const string AccommodationTypeCodeExists = "Тип размещения с таким кодом уже существует.";

        // HotelCategory
        public const string HotelCategoryNotFound = "Категория отеля не найдена.";
        public const string HotelCategoryNameExists = "Категория отеля с таким названием уже существует.";
        public const string HotelCategoryGlobalCodeExists = "Категория отеля с таким глобальным кодом уже существует.";

        // Rate
        public const string RateNotFound = "Валюта не найдена.";
        public const string RateCodeExists = "Валюта с таким кодом уже существует.";
        public const string RateIsoCodeExists = "Валюта с таким ISO кодом уже существует.";

        // RealCourse
        public const string RealCourseNotFound = "Курс валют не найден.";
        public const string RealCourseExists = "Курс для выбранной пары валют и периода уже существует.";
        public const string FromRateNotFound = "Исходная валюта не найдена.";
        public const string ToRateNotFound = "Целевая валюта не найдена.";

        // AirClass
        public const string AirClassNotFound = "Класс обслуживания не найден.";
        public const string AirClassCodeExists = "Класс обслуживания с таким кодом уже существует.";

        // Aircraft
        public const string AircraftNotFound = "Воздушное судно не найдено.";
        public const string AircraftCodeExists = "Воздушное судно с таким кодом уже существует.";

        // Airline
        public const string AirlineNotFound = "Авиакомпания не найдена.";
        public const string AirlineCodeExists = "Авиакомпания с таким кодом уже существует.";
        public const string AirlineIcaoCodeExists = "Авиакомпания с таким ICAO кодом уже существует.";

        // Airport
        public const string AirportNotFound = "Аэропорт не найден.";
        public const string AirportCodeExists = "Аэропорт с таким кодом уже существует.";
        public const string AirportIcaoCodeExists = "Аэропорт с таким ICAO кодом уже существует.";

        // ============================================================
        // Общие сообщения для маршрутов (авиа, автобус и т.д.)
        // ============================================================
        public const string DepartureCityNotFound = "Город отправления не найден.";
        public const string ArrivalCityNotFound = "Город прибытия не найден.";
        public const string SameCities = "Город отправления и город прибытия должны отличаться.";

        // Charter
        public const string CharterNotFound = "Чартер не найден.";
        public const string DepartureAirportNotFound = "Аэропорт отправления не найден.";
        public const string ArrivalAirportNotFound = "Аэропорт прибытия не найден.";

        // CharterSeason
        public const string CharterSeasonNotFound = "Сезон чартера не найден.";

        // BusTransfer
        public const string BusTransferNotFound = "Автобусный переезд не найден.";
        public const string DepartureCountryNotFound = "Страна отправления не найдена.";
        public const string ArrivalCountryNotFound = "Страна прибытия не найдена.";
        public const string DepartureCityCountryMismatch = "Город отправления не принадлежит выбранной стране отправления.";
        public const string ArrivalCityCountryMismatch = "Город прибытия не принадлежит выбранной стране прибытия.";

        // BusTransferPoint
        public const string BusTransferPointNotFound = "Точка автобусного переезда не найдена.";

        // BusSchedule
        public const string BusScheduleNotFound = "Расписание автобусного переезда не найдено.";

        //  RailwayTransfer
        public const string RailwayTransferNotFound = "ЖД-переезд не найден.";

        // Расписание ЖД-переезда
        public const string TrainScheduleNotFound = "Расписание ЖД-переезда не найдено.";

        // Транспорт
        public const string TransportNotFound = "Транспорт не найден.";

        // Трансфер
        public const string TransferNotFound = "Трансфер не найден.";
        public const string TransferDirectionNotFound = "Направление трансфера не найдено.";

        // Рассадка
        public const string SeatingCellNotFound = "Ячейка схемы размещения не найдена.";
        public const string VehiclePlanNotFound = "Схема транспорта не найдена.";

        // Правило размещения
        public const string AccommodationPlacementRuleNotFound = "Правило размещения не найдено.";
        public const string AccommodationPlacementRuleAgeRangeNotFound = "Возрастной диапазон правила размещения не найден.";
    }
}