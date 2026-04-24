using System.Collections.Generic;
using TourCore.Application.Charters.Commands;
using TourCore.Application.Common.Exceptions;

namespace TourCore.Application.Charters.Validators
{
    public class CreateCharterCommandValidator
    {
        public void ValidateAndThrow(CreateCharterCommand command)
        {
            var errors = Validate(command);
            if (errors.Count > 0)
                throw new ValidationException(errors);
        }

        public IReadOnlyCollection<string> Validate(CreateCharterCommand command)
        {
            var errors = new List<string>();

            if (command == null)
            {
                errors.Add("Command is required.");
                return errors;
            }

            if (command.DepartureCityId <= 0)
                errors.Add("DepartureCityId must be greater than 0.");

            if (command.ArrivalCityId <= 0)
                errors.Add("ArrivalCityId must be greater than 0.");

            if (command.DepartureCityId > 0 &&
                command.ArrivalCityId > 0 &&
                command.DepartureCityId == command.ArrivalCityId)
            {
                errors.Add("DepartureCityId and ArrivalCityId must be different.");
            }

            if (!string.IsNullOrWhiteSpace(command.DepartureAirportCode) && command.DepartureAirportCode.Trim().Length > 4)
                errors.Add("DepartureAirportCode must be 4 characters or less.");

            if (!string.IsNullOrWhiteSpace(command.ArrivalAirportCode) && command.ArrivalAirportCode.Trim().Length > 4)
                errors.Add("ArrivalAirportCode must be 4 characters or less.");

            if (string.IsNullOrWhiteSpace(command.FlightNumber))
                errors.Add("FlightNumber is required.");
            else if (command.FlightNumber.Trim().Length > 4)
                errors.Add("FlightNumber must be 4 characters or less.");

            if (!string.IsNullOrWhiteSpace(command.AirlineCode) && command.AirlineCode.Trim().Length > 3)
                errors.Add("AirlineCode must be 3 characters or less.");

            if (!string.IsNullOrWhiteSpace(command.AircraftCode) && command.AircraftCode.Trim().Length > 3)
                errors.Add("AircraftCode must be 3 characters or less.");

            if (!string.IsNullOrWhiteSpace(command.AirClassCode) && command.AirClassCode.Trim().Length > 10)
                errors.Add("AirClassCode must be 10 characters or less.");

            if (command.StopsCount.HasValue && command.StopsCount.Value < 0)
                errors.Add("StopsCount cannot be negative.");

            if (!string.IsNullOrWhiteSpace(command.TimeChangesCode) && command.TimeChangesCode.Trim().Length > 1)
                errors.Add("TimeChangesCode must be 1 character or less.");

            return errors;
        }
    }
}