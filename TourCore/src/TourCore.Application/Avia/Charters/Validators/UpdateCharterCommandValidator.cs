using System.Collections.Generic;
using System.Linq;
using TourCore.Application.Avia.Charters.Commands;
using TourCore.Application.Common.Errors;
using TourCore.Application.Common.Exceptions;

namespace TourCore.Application.Avia.Charters.Validators
{
    public class UpdateCharterCommandValidator
    {
        public void ValidateAndThrow(UpdateCharterCommand command)
        {
            var errors = Validate(command);

            if (errors.Count > 0)
                throw new ValidationException(errors);
        }

        public IReadOnlyDictionary<string, string[]> Validate(UpdateCharterCommand command)
        {
            var errors = new Dictionary<string, List<string>>();

            if (command == null)
            {
                AddError(errors, "General", ErrorCode.Required);
                return ToResult(errors);
            }

            if (command.Id <= 0)
                AddError(errors, "Id", ErrorCode.GreaterThanZero);

            CreateCharterCommandValidator.ValidateCommon(
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