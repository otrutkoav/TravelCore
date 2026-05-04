using System.Collections.Generic;
using System.Linq;
using TourCore.Application.Bus.BusSchedules.Commands;
using TourCore.Application.Common.Errors;
using TourCore.Application.Common.Exceptions;

namespace TourCore.Application.Bus.BusSchedules.Validators
{
    public class UpdateBusScheduleCommandValidator
    {
        public void ValidateAndThrow(UpdateBusScheduleCommand command)
        {
            var errors = Validate(command);

            if (errors.Count > 0)
                throw new ValidationException(errors);
        }

        public IReadOnlyDictionary<string, string[]> Validate(UpdateBusScheduleCommand command)
        {
            var errors = new Dictionary<string, List<string>>();

            if (command == null)
            {
                AddError(errors, "General", ErrorCode.Required);
                return ToResult(errors);
            }

            if (command.Id <= 0)
                AddError(errors, "Id", ErrorCode.GreaterThanZero);

            if (command.BusTransferId <= 0)
                AddError(errors, "BusTransferId", ErrorCode.GreaterThanZero);

            if (command.DateFrom.HasValue &&
                command.DateTo.HasValue &&
                command.DateFrom.Value.Date > command.DateTo.Value.Date)
            {
                AddError(errors, "DateFrom", ErrorCode.DateRangeInvalid);
            }

            if (command.DaysOnRoad.HasValue && command.DaysOnRoad.Value < 0)
                AddError(errors, "DaysOnRoad", ErrorCode.Negative);

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