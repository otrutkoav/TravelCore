using System.Collections.Generic;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Transports.Commands;

namespace TourCore.Application.Transports.Validators
{
    public class UpdateTransportCommandValidator
    {
        public void ValidateAndThrow(UpdateTransportCommand command)
        {
            var errors = Validate(command);
            if (errors.Count > 0)
                throw new ValidationException(errors);
        }

        public IReadOnlyCollection<string> Validate(UpdateTransportCommand command)
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
            else if (command.Name.Trim().Length > 100)
                errors.Add("Name must be 100 characters or less.");

            if (!string.IsNullOrWhiteSpace(command.NameEn) && command.NameEn.Trim().Length > 100)
                errors.Add("NameEn must be 100 characters or less.");

            if (command.SeatsCount.HasValue && command.SeatsCount.Value < 0)
                errors.Add("SeatsCount cannot be negative.");

            if (command.ShowOrder < 0)
                errors.Add("ShowOrder cannot be negative.");

            return errors;
        }
    }
}