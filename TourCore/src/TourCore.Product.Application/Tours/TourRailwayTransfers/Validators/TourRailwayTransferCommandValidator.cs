using System;
using System.Collections.Generic;
using System.Linq;
using TourCore.Application.Common.Exceptions;

namespace TourCore.Application.Tours
{
    public class TourRailwayTransferCommandValidator
    {
        public void ValidateAndThrow(ReplaceTourRailwayTransferCommand command)
        {
            if (command == null)
                throw new ValidationException(new[] { "Request body is required." });
            var errors = new List<string>();
            ValidateRequired(command.DepartureCountryIds, "DepartureCountryIds", errors);
            ValidateRequired(command.DepartureCityIds, "DepartureCityIds", errors);
            ValidateRequired(command.ArrivalCountryIds, "ArrivalCountryIds", errors);
            ValidateRequired(command.ArrivalCityIds, "ArrivalCityIds", errors);
            ValidateRequired(command.RailwayTransferIds, "RailwayTransferIds", errors);
            ValidateOptional(command.TransportIds, "TransportIds", errors);
            ValidateTime(command.DepartureTimeFrom, command.DepartureTimeTo, "Departure", errors);
            ValidateTime(command.ArrivalTimeFrom, command.ArrivalTimeTo, "Arrival", errors);
            if (errors.Count > 0)
                throw new ValidationException(errors);
        }

        private static void ValidateRequired(
            IEnumerable<int> ids,
            string name,
            ICollection<string> errors
        )
        {
            var values = (ids ?? Enumerable.Empty<int>()).ToArray();
            if (values.Length == 0)
                errors.Add($"{name} must contain at least one identifier.");
            ValidateValues(values, name, errors);
        }

        private static void ValidateOptional(
            IEnumerable<int> ids,
            string name,
            ICollection<string> errors
        ) => ValidateValues((ids ?? Enumerable.Empty<int>()).ToArray(), name, errors);

        private static void ValidateValues(int[] values, string name, ICollection<string> errors)
        {
            if (values.Any(x => x <= 0))
                errors.Add($"{name} must contain only positive identifiers.");
            if (values.Distinct().Count() != values.Length)
                errors.Add($"{name} must not contain duplicates.");
        }

        private static void ValidateTime(
            TimeSpan from,
            TimeSpan to,
            string name,
            ICollection<string> errors
        )
        {
            if (from < TimeSpan.Zero || from >= TimeSpan.FromDays(1))
                errors.Add($"{name}TimeFrom must be within one day.");
            if (to < TimeSpan.Zero || to >= TimeSpan.FromDays(1))
                errors.Add($"{name}TimeTo must be within one day.");
            if (from > to)
                errors.Add($"{name}TimeFrom cannot exceed {name}TimeTo.");
        }
    }
}
