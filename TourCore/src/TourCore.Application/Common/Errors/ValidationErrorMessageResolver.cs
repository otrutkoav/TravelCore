using TourCore.Application.Common.Errors;

namespace TourCore.Api.Legacy.Models
{
    public static class ValidationErrorMessageResolver
    {
        public static string Resolve(string code)
        {
            switch (code)
            {
                case ErrorCode.Required:
                    return "Поле обязательно для заполнения.";

                case ErrorCode.MaxLength:
                    return "Значение превышает допустимую длину.";

                case ErrorCode.ExactLength:
                    return "Значение имеет некорректную длину.";

                case ErrorCode.GreaterThanZero:
                    return "Значение должно быть больше нуля.";

                case ErrorCode.Negative:
                    return "Значение не может быть отрицательным.";

                case ErrorCode.DateRangeInvalid:
                    return "Начальная дата не может быть больше конечной.";

                case ErrorCode.SameRateCodes:
                    return "Исходная и целевая валюты должны отличаться.";

                case ErrorCode.SameCities:
                    return "Город отправления и город прибытия должны отличаться.";

                case ErrorCode.DepartureCityCountryMismatch:
                    return "Город отправления не принадлежит выбранной стране отправления.";

                case ErrorCode.ArrivalCityCountryMismatch:
                    return "Город прибытия не принадлежит выбранной стране прибытия.";
                case ErrorCode.AgeRangeInvalid:
                    return "Возраст от не может быть больше возраста до.";

                default:
                    return $"Неизвестная ошибка валидации ({code}).";
            }
        }
    }
}