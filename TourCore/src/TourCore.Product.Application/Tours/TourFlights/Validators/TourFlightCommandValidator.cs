using System.Collections.Generic;
using System.Linq;
using TourCore.Application.Common.Exceptions;

namespace TourCore.Application.Tours
{
    public class TourFlightCommandValidator
    {
        public void ValidateAndThrow(ReplaceTourFlightsCommand command)
        {
            var errors = new List<string>();

            if (command == null)
            {
                errors.Add("Request body is required.");
                throw new ValidationException(errors);
            }

            var groups = (command.Groups ?? new List<TourFlightGroupInput>()).ToArray();
            ValidateOrderIndexes(groups.Select(x => x.OrderIndex), "Group", errors);

            foreach (var group in groups)
            {
                if (string.IsNullOrWhiteSpace(group.Name))
                    errors.Add("Flight group name is required.");
                else if (group.Name.Trim().Length > 128)
                    errors.Add("Flight group name must be 128 characters or less.");

                if (group.Priority <= 0)
                    errors.Add("Flight group priority must be greater than zero.");

                var flights = (group.Flights ?? new List<TourFlightInput>()).ToArray();
                if (flights.Length == 0)
                {
                    errors.Add("Every flight group must contain at least one flight.");
                    continue;
                }

                ValidateOrderIndexes(flights.Select(x => x.OrderIndex), "Flight", errors);

                foreach (var flight in flights)
                {
                    ValidateIds(flight.DepartureCountryIds, "DepartureCountryIds", errors);
                    ValidateIds(flight.DepartureCityIds, "DepartureCityIds", errors);
                    ValidateIds(flight.ArrivalCountryIds, "ArrivalCountryIds", errors);
                    ValidateIds(flight.ArrivalCityIds, "ArrivalCityIds", errors);
                    ValidateIds(flight.AirlineIds, "AirlineIds", errors);
                    ValidateIds(flight.FlightIds, "FlightIds", errors);
                    ValidateIds(flight.DepartureAirportIds, "DepartureAirportIds", errors);
                    ValidateIds(flight.ArrivalAirportIds, "ArrivalAirportIds", errors);
                    ValidateIds(flight.FareIds, "FareIds", errors);

                    if (
                        flight.OrderIndex == 1
                        && (
                            flight.MinConnectionMinutes.HasValue
                            || flight.MaxConnectionMinutes.HasValue
                        )
                    )
                        errors.Add("The first flight cannot have a connection interval.");

                    if (
                        flight.MinConnectionMinutes.HasValue != flight.MaxConnectionMinutes.HasValue
                    )
                        errors.Add("Both connection interval limits must be specified.");
                    else if (flight.MinConnectionMinutes < 0 || flight.MaxConnectionMinutes < 0)
                        errors.Add("Connection interval cannot be negative.");
                    else if (flight.MinConnectionMinutes > flight.MaxConnectionMinutes)
                        errors.Add("Minimum connection interval cannot exceed maximum.");
                }
            }

            if (errors.Count > 0)
                throw new ValidationException(errors);
        }

        private static void ValidateOrderIndexes(
            IEnumerable<int> indexes,
            string entityName,
            ICollection<string> errors
        )
        {
            var values = indexes.OrderBy(x => x).ToArray();
            var expected = Enumerable.Range(1, values.Length).ToArray();

            if (!values.SequenceEqual(expected))
                errors.Add($"{entityName} OrderIndex values must form the sequence 1..N.");
        }

        private static void ValidateIds(
            IEnumerable<int> ids,
            string propertyName,
            ICollection<string> errors
        )
        {
            if (ids != null && ids.Any(x => x <= 0))
                errors.Add($"{propertyName} must contain only positive identifiers.");
        }
    }
}
