using System.Collections.Generic;
using TourCore.Application.AirClasses.Commands;
using TourCore.Application.Common.Exceptions;

namespace TourCore.Application.AirClasses.Validators
{
    public class UpdateAirClassCommandValidator
    {
        public void ValidateAndThrow(UpdateAirClassCommand command)
        {
            var errors = Validate(command);
            if (errors.Count > 0)
                throw new ValidationException(errors);
        }

        public IReadOnlyCollection<string> Validate(UpdateAirClassCommand command)
        {
            var errors = new List<string>();

            if (command == null)
            {
                errors.Add("Command is required.");
                return errors;
            }

            if (command.Id <= 0)
                errors.Add("Id is required.");

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

            if (!string.IsNullOrWhiteSpace(command.Group) && command.Group.Trim().Length > 128)
                errors.Add("Group must be 128 characters or less.");

            if (command.SortOrder < 0)
                errors.Add("SortOrder cannot be negative.");

            return errors;
        }
    }
}