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
    }
}