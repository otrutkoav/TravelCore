using System.Collections.Generic;
using System.Linq;
using TourCore.Application.Avia.Charters.Commands;
using TourCore.Application.Common.Errors;
using TourCore.Application.Common.Exceptions;

namespace TourCore.Application.Avia.Charters.Validators
{
    public class CreateCharterCommandValidator
    {
        public void ValidateAndThrow(CreateCharterCommand command)
        {
            var errors = Validate(command);

            if (errors.Count > 0)
                throw new ValidationException(errors);
        }

        public IReadOnlyDictionary<string, string[]> Validate(CreateCharterCommand command)
        {
            var errors = new Dictionary<string, List<string>>();

            if (command == null)
            {
                AddError(errors, "General", ErrorCode.Required);
                return ToResult(errors);
            }

            ValidateCommon(
                command.DepartureCityId,
                command.ArrivalCityId,
                command.DepartureAirportCode,
                command.ArrivalAirportCode,
                command.FlightNumber,
                command.AirlineCode,
                command.AircraftCode,
                command.AirClassCode,
                command.StopsCount,
                command.TimeChangesCode,
                errors);

            return ToResult(errors);
        }

        internal static void ValidateCommon(
            int departureCityId,
            int arrivalCityId,
            string departureAirportCode,
            string arrivalAirportCode,
            string flightNumber,
            string airlineCode,
            string aircraftCode,
            string airClassCode,
            short? stopsCount,
            string timeChangesCode,
            IDictionary<string, List<string>> errors)
        {
            if (departureCityId <= 0)
                AddError(errors, "DepartureCityId", ErrorCode.GreaterThanZero);

            if (arrivalCityId <= 0)
                AddError(errors, "ArrivalCityId", ErrorCode.GreaterThanZero);

            if (departureCityId > 0 &&
                arrivalCityId > 0 &&
                departureCityId == arrivalCityId)
            {
                AddError(errors, "ArrivalCityId", ErrorCode.SameCities);
            }

            if (!string.IsNullOrWhiteSpace(departureAirportCode) &&
                departureAirportCode.Trim().Length > 4)
            {
                AddError(errors, "DepartureAirportCode", ErrorCode.MaxLength);
            }

            if (!string.IsNullOrWhiteSpace(arrivalAirportCode) &&
                arrivalAirportCode.Trim().Length > 4)
            {
                AddError(errors, "ArrivalAirportCode", ErrorCode.MaxLength);
            }

            if (string.IsNullOrWhiteSpace(flightNumber))
                AddError(errors, "FlightNumber", ErrorCode.Required);
            else if (flightNumber.Trim().Length > 4)
                AddError(errors, "FlightNumber", ErrorCode.MaxLength);

            if (!string.IsNullOrWhiteSpace(airlineCode) &&
                airlineCode.Trim().Length > 3)
            {
                AddError(errors, "AirlineCode", ErrorCode.MaxLength);
            }

            if (!string.IsNullOrWhiteSpace(aircraftCode) &&
                aircraftCode.Trim().Length > 3)
            {
                AddError(errors, "AircraftCode", ErrorCode.MaxLength);
            }

            if (!string.IsNullOrWhiteSpace(airClassCode) &&
                airClassCode.Trim().Length > 10)
            {
                AddError(errors, "AirClassCode", ErrorCode.MaxLength);
            }

            if (stopsCount.HasValue && stopsCount.Value < 0)
                AddError(errors, "StopsCount", ErrorCode.Negative);

            if (!string.IsNullOrWhiteSpace(timeChangesCode) &&
                timeChangesCode.Trim().Length > 1)
            {
                AddError(errors, "TimeChangesCode", ErrorCode.MaxLength);
            }
        }

        private static void AddError(
            IDictionary<string, List<string>> errors,
            string field,
            string code)
        {
            if (!errors.ContainsKey(field))
                errors[field] = new List<string>();

            errors[field].Add(code);
        }

        private static IReadOnlyDictionary<string, string[]> ToResult(
            IDictionary<string, List<string>> errors)
        {
            return errors.ToDictionary(
                x => x.Key,
                x => x.Value.ToArray());
        }
    }
}