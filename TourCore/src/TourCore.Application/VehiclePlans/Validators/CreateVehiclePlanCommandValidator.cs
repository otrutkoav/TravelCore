using System.Collections.Generic;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.VehiclePlans.Commands;

namespace TourCore.Application.VehiclePlans.Validators
{
    public class CreateVehiclePlanCommandValidator
    {
        public void ValidateAndThrow(CreateVehiclePlanCommand command)
        {
            var errors = Validate(command);
            if (errors.Count > 0)
                throw new ValidationException(errors);
        }

        public IReadOnlyCollection<string> Validate(CreateVehiclePlanCommand command)
        {
            var errors = new List<string>();

            if (command == null)
            {
                errors.Add("Command is required.");
                return errors;
            }

            if (command.TransportId <= 0)
                errors.Add("TransportId must be greater than 0.");

            if (command.RowsCount <= 0)
                errors.Add("RowsCount must be greater than 0.");

            if (command.ColumnsCount <= 0)
                errors.Add("ColumnsCount must be greater than 0.");

            if (command.AreaNumber <= 0)
                errors.Add("AreaNumber must be greater than 0.");

            if (!string.IsNullOrWhiteSpace(command.Name) && command.Name.Trim().Length > 20)
                errors.Add("Name must be 20 characters or less.");

            if (!string.IsNullOrWhiteSpace(command.Comment) && command.Comment.Trim().Length > 250)
                errors.Add("Comment must be 250 characters or less.");

            return errors;
        }
    }
}