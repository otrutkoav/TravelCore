using System.Collections.Generic;
using System.Linq;
using TourCore.Application.Common.Errors;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Seating.SeatingCells.Commands;

namespace TourCore.Application.Seating.SeatingCells.Validators
{
    public class UpdateSeatingCellCommandValidator
    {
        public void ValidateAndThrow(UpdateSeatingCellCommand command)
        {
            var errors = Validate(command);

            if (errors.Count > 0)
                throw new ValidationException(errors);
        }

        public IReadOnlyDictionary<string, string[]> Validate(UpdateSeatingCellCommand command)
        {
            var errors = new Dictionary<string, List<string>>();

            if (command == null)
            {
                AddError(errors, "General", ErrorCode.Required);
                return ToResult(errors);
            }

            if (command.Id <= 0)
                AddError(errors, "Id", ErrorCode.GreaterThanZero);

            if (command.VehiclePlanId <= 0)
                AddError(errors, "VehiclePlanId", ErrorCode.GreaterThanZero);

            if (command.Index < 0)
                AddError(errors, "Index", ErrorCode.Negative);

            if (!string.IsNullOrWhiteSpace(command.Number) && command.Number.Trim().Length > 4)
                AddError(errors, "Number", ErrorCode.MaxLength);

            if (command.SeatsCount.HasValue && command.SeatsCount.Value < 0)
                AddError(errors, "SeatsCount", ErrorCode.Negative);

            if (!string.IsNullOrWhiteSpace(command.Border) && command.Border.Trim().Length > 4)
                AddError(errors, "Border", ErrorCode.MaxLength);

            return ToResult(errors);
        }

        private static void AddError(IDictionary<string, List<string>> errors, string field, string code)
        {
            if (!errors.ContainsKey(field))
                errors[field] = new List<string>();

            errors[field].Add(code);
        }

        private static IReadOnlyDictionary<string, string[]> ToResult(IDictionary<string, List<string>> errors)
        {
            return errors.ToDictionary(x => x.Key, x => x.Value.ToArray());
        }
    }
}