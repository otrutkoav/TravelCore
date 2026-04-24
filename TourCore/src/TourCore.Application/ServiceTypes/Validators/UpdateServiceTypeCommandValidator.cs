using System.Collections.Generic;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.ServiceTypes.Commands;

namespace TourCore.Application.ServiceTypes.Validators
{
    public class UpdateServiceTypeCommandValidator
    {
        public void ValidateAndThrow(UpdateServiceTypeCommand command)
        {
            var errors = Validate(command);
            if (errors.Count > 0)
                throw new ValidationException(errors);
        }

        public IReadOnlyCollection<string> Validate(UpdateServiceTypeCommand command)
        {
            var errors = new List<string>();

            if (command == null)
            {
                errors.Add("Command is required.");
                return errors;
            }

            if (command.Id <= 0)
                errors.Add("Id must be greater than 0.");

            if (string.IsNullOrWhiteSpace(command.Name))
                errors.Add("Name is required.");
            else if (command.Name.Trim().Length > 50)
                errors.Add("Name must be 50 characters or less.");

            if (!string.IsNullOrWhiteSpace(command.NameEn) && command.NameEn.Trim().Length > 50)
                errors.Add("NameEn must be 50 characters or less.");

            if (!string.IsNullOrWhiteSpace(command.Code) && command.Code.Trim().Length > 10)
                errors.Add("Code must be 10 characters or less.");

            if (command.Category.HasValue && command.Category.Value < 0)
                errors.Add("Category cannot be negative.");

            if (command.ControlMode.HasValue && command.ControlMode.Value < 0)
                errors.Add("ControlMode cannot be negative.");

            if (command.SmallAmountPercent.HasValue && command.SmallAmountPercent.Value < 0)
                errors.Add("SmallAmountPercent cannot be negative.");

            if (command.SmallAmountThreshold.HasValue && command.SmallAmountThreshold.Value < 0)
                errors.Add("SmallAmountThreshold cannot be negative.");

            return errors;
        }
    }
}