namespace TourCore.Application.Common.Errors
{
    public static class ErrorMessages
    {
        public const string ValidationError = "Ошибка валидации.";
        public const string InternalError = "Внутренняя ошибка сервера.";

        public const string CountryNotFound = "Страна не найдена.";
        public const string CountryCodeExists = "Страна с таким кодом уже существует.";
        public const string CountryIsoCode2Exists = "Страна с таким ISO2 кодом уже существует.";
        public const string CountryIsoCode3Exists = "Страна с таким ISO3 кодом уже существует.";
    }
}