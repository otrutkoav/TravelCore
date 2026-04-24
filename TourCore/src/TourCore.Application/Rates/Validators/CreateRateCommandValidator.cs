using System.Collections.Generic;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Rates.Commands;

namespace TourCore.Application.Rates.Validators
{
    public class CreateRateCommandValidator
    {
        public void ValidateAndThrow(CreateRateCommand command)
        {
            var errors = Validate(command);
            if (errors.Count > 0)
                throw new ValidationException(errors);
        }

        public IReadOnlyCollection<string> Validate(CreateRateCommand command)
        {
            var errors = new List<string>();

            if (command == null)
            {
                errors.Add("Command is required.");
                return errors;
            }

            if (string.IsNullOrWhiteSpace(command.Code))
                errors.Add("Code is required.");
            else if (command.Code.Trim().Length > 3)
                errors.Add("Code must be 3 characters or less.");

            if (string.IsNullOrWhiteSpace(command.Name))
                errors.Add("Name is required.");
            else if (command.Name.Trim().Length > 50)
                errors.Add("Name must be 50 characters or less.");

            if (!string.IsNullOrWhiteSpace(command.IsoCode) && command.IsoCode.Trim().Length > 3)
                errors.Add("IsoCode must be 3 characters or less.");

            return errors;
        }
    }
}