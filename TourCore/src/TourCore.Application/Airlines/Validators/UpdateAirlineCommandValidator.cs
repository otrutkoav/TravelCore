using System.Collections.Generic;
using TourCore.Application.Airlines.Commands;
using TourCore.Application.Common.Exceptions;

namespace TourCore.Application.Airlines.Validators
{
    public class UpdateAirlineCommandValidator
    {
        public void ValidateAndThrow(UpdateAirlineCommand command)
        {
            var errors = Validate(command);
            if (errors.Count > 0)
                throw new ValidationException(errors);
        }

        public IReadOnlyCollection<string> Validate(UpdateAirlineCommand command)
        {
            var errors = new List<string>();

            if (command == null)
            {
                errors.Add("Command is required.");
                return errors;
            }

            if (command.Id <= 0)
                errors.Add("Id must be greater than 0.");

            if (string.IsNullOrWhiteSpace(command.Code))
                errors.Add("Code is required.");
            else if (command.Code.Trim().Length > 3)
                errors.Add("Code must be 3 characters or less.");

            if (string.IsNullOrWhiteSpace(command.Name))
                errors.Add("Name is required.");
            else if (command.Name.Trim().Length > 100)
                errors.Add("Name must be 100 characters or less.");

            if (!string.IsNullOrWhiteSpace(command.NameEn) && command.NameEn.Trim().Length > 100)
                errors.Add("NameEn must be 100 characters or less.");

            if (!string.IsNullOrWhiteSpace(command.IcaoCode) && command.IcaoCode.Trim().Length > 4)
                errors.Add("IcaoCode must be 4 characters or less.");

            return errors;
        }
    }
}