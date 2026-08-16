using System;
using System.Collections.Generic;
using System.Linq;
using TourCore.Application.Common.Exceptions;

namespace TourCore.Application.Tours
{
    public class TourBusTransferCommandValidator
    {
        public void ValidateAndThrow(ReplaceTourBusTransferCommand command)
        {
            if (command == null)
                throw new ValidationException(new[] { "Request body is required." });

            var errors = new List<string>();
            ValidateRequiredIds(command.DepartureCountryIds, "DepartureCountryIds", errors);
            ValidateRequiredIds(command.DepartureCityIds, "DepartureCityIds", errors);
            ValidateRequiredIds(command.ArrivalCountryIds, "ArrivalCountryIds", errors);
            ValidateRequiredIds(command.ArrivalCityIds, "ArrivalCityIds", errors);
            ValidateRequiredIds(command.BusTransferIds, "BusTransferIds", errors);
            ValidateOptionalIds(command.BoardingPointIds, "BoardingPointIds", errors);
            ValidateOptionalIds(command.TransportIds, "TransportIds", errors);
            ValidateTimeRange(
                command.DepartureTimeFrom,
                command.DepartureTimeTo,
                "Departure",
                errors
            );
            ValidateTimeRange(command.ArrivalTimeFrom, command.ArrivalTimeTo, "Arrival", errors);

            if (errors.Count > 0)
                throw new ValidationException(errors);
        }

        private static void ValidateRequiredIds(
            IEnumerable<int> ids,
            string propertyName,
            ICollection<string> errors
        )
        {
            var values = (ids ?? Enumerable.Empty<int>()).ToArray();
            if (values.Length == 0)
                errors.Add($"{propertyName} must contain at least one identifier.");
            ValidateValues(values, propertyName, errors);
        }

        private static void ValidateOptionalIds(
            IEnumerable<int> ids,
            string propertyName,
            ICollection<string> errors
        )
        {
            ValidateValues((ids ?? Enumerable.Empty<int>()).ToArray(), propertyName, errors);
        }

        private static void ValidateValues(
            int[] values,
            string propertyName,
            ICollection<string> errors
        )
        {
            if (values.Any(x => x <= 0))
                errors.Add($"{propertyName} must contain only positive identifiers.");
            if (values.Distinct().Count() != values.Length)
                errors.Add($"{propertyName} must not contain duplicates.");
        }

        private static void ValidateTimeRange(
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
