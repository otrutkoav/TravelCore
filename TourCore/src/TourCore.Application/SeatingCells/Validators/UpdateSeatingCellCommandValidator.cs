using System.Collections.Generic;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.SeatingCells.Commands;

namespace TourCore.Application.SeatingCells.Validators
{
    public class UpdateSeatingCellCommandValidator
    {
        public void ValidateAndThrow(UpdateSeatingCellCommand command)
        {
            var errors = Validate(command);
            if (errors.Count > 0)
                throw new ValidationException(errors);
        }

        public IReadOnlyCollection<string> Validate(UpdateSeatingCellCommand command)
        {
            var errors = new List<string>();

            if (command == null)
            {
                errors.Add("Command is required.");
                return errors;
            }

            if (command.Id <= 0)
                errors.Add("Id must be greater than 0.");

            if (command.VehiclePlanId <= 0)
                errors.Add("VehiclePlanId must be greater than 0.");

            if (command.Index < 0)
                errors.Add("Index cannot be negative.");

            if (!string.IsNullOrWhiteSpace(command.Number) && command.Number.Trim().Length > 4)
                errors.Add("Number must be 4 characters or less.");

            if (command.SeatsCount.HasValue && command.SeatsCount.Value < 0)
                errors.Add("SeatsCount cannot be negative.");

            if (!string.IsNullOrWhiteSpace(command.Border) && command.Border.Trim().Length > 4)
                errors.Add("Border must be 4 characters or less.");

            return errors;
        }
    }
}