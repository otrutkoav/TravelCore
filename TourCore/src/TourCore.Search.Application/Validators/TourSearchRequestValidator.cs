using System;
using System.Collections.Generic;
using System.Linq;
using TourCore.Application.Common.Exceptions;
using TourCore.Search.Contracts;

namespace TourCore.Search.Application.Validators
{
    public sealed class TourSearchRequestValidator
    {
        public void ValidateAndNormalize(TourSearchRequest request)
        {
            if (request == null)
                throw new ValidationException(new Dictionary<string, ValidationError[]>
                {
                    { "Request", new[] { Error("required", "Тело запроса не передано.") } }
                });

            var errors = new Dictionary<string, ValidationError[]>();
            if (request.DepartureCityId == 0 || request.DepartureCityId < -1) errors.Add("DepartureCityId", new[] { Error("invalid_departure_city", "Город отправления обязателен; -1 означает поиск без транспорта.") });
            if (request.DestinationCountryId <= 0) errors.Add("DestinationCountryId", new[] { Error("required", "Страна назначения обязательна.") });
            if (request.DestinationCityId.HasValue && request.DestinationCityId.Value <= 0) errors.Add("DestinationCityId", new[] { Error("invalid_destination_city", "Некорректный город назначения.") });
            if (request.Dates == null || request.Dates.Count < 1 || request.Dates.Count > 10) errors.Add("Dates", new[] { Error("count_out_of_range", "Нужно передать от 1 до 10 дат.") });
            if (request.DurationsInNights == null || request.DurationsInNights.Count < 1 || request.DurationsInNights.Count > 10) errors.Add("DurationsInNights", new[] { Error("count_out_of_range", "Нужно передать от 1 до 10 продолжительностей.") });
            else if (request.DurationsInNights.Any(x => x < 1 || x > 60)) errors.Add("DurationsInNights", new[] { Error("value_out_of_range", "Продолжительность должна быть от 1 до 60 ночей.") });
            if (request.Adults < 1 || request.Adults > 20) errors.Add("Adults", new[] { Error("value_out_of_range", "Количество взрослых должно быть от 1 до 20.") });
            if (errors.Count > 0) throw new ValidationException(errors);

            request.Dates = request.Dates.Select(x => x.Date).Distinct().OrderBy(x => x).ToList();
            request.DurationsInNights = request.DurationsInNights.Distinct().OrderBy(x => x).ToList();
            request.Page = request.Page < 1 ? 1 : request.Page;
            request.PageSize = request.PageSize < 1 ? 20 : Math.Min(request.PageSize, 100);
        }

        private static ValidationError Error(string code, string message)
        {
            return new ValidationError(code, message);
        }
    }
}
