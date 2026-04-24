using System.Collections.Generic;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.ServiceTypes.Commands;

namespace TourCore.Application.ServiceTypes.Validators
{
    public class CreateServiceTypeCommandValidator
    {
        public void ValidateAndThrow(CreateServiceTypeCommand command)
        {
            var errors = Validate(command);
            if (errors.Count > 0)
                throw new ValidationException(errors);
        }

        public IReadOnlyCollection<string> Validate(CreateServiceTypeCommand command)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(command.Name))
                errors.Add("Name is required.");
            else if (command.Name.Trim().Length > 50)
                errors.Add("Name must be 50 characters or less.");

            if (!string.IsNullOrWhiteSpace(command.Code) && command.Code.Trim().Length > 10)
                errors.Add("Code must be 10 characters or less.");

            if (command.Category.HasValue && command.Category < 0)
                errors.Add("Category cannot be negative.");

            if (command.ControlMode.HasValue && command.ControlMode < 0)
                errors.Add("ControlMode cannot be negative.");

            if (command.SmallAmountPercent.HasValue && command.SmallAmountPercent < 0)
                errors.Add("SmallAmountPercent cannot be negative.");

            if (command.SmallAmountThreshold.HasValue && command.SmallAmountThreshold < 0)
                errors.Add("SmallAmountThreshold cannot be negative.");

            return errors;
        }
    }
}