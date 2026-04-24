using System.Collections.Generic;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Countries.Commands;

namespace TourCore.Application.Countries.Validators
{
    public class UpdateCountryCommandValidator
    {
        public void ValidateAndThrow(UpdateCountryCommand command)
        {
            var errors = Validate(command);
            if (errors.Count > 0)
                throw new ValidationException(errors);
        }

        public IReadOnlyCollection<string> Validate(UpdateCountryCommand command)
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
            else if (command.Name.Trim().Length > 25)
                errors.Add("Name must be 25 characters or less.");

            if (!string.IsNullOrWhiteSpace(command.NameEn) && command.NameEn.Trim().Length > 25)
                errors.Add("NameEn must be 25 characters or less.");

            if (string.IsNullOrWhiteSpace(command.Code))
                errors.Add("Code is required.");
            else if (command.Code.Trim().Length > 3)
                errors.Add("Code must be 3 characters or less.");

            if (string.IsNullOrWhiteSpace(command.IsoCode2))
                errors.Add("IsoCode2 is required.");
            else if (command.IsoCode2.Trim().Length != 2)
                errors.Add("IsoCode2 must contain exactly 2 characters.");

            if (string.IsNullOrWhiteSpace(command.IsoCode3))
                errors.Add("IsoCode3 is required.");
            else if (command.IsoCode3.Trim().Length != 3)
                errors.Add("IsoCode3 must contain exactly 3 characters.");

            if (command.SortOrder < 0)
                errors.Add("SortOrder cannot be negative.");

            if (!string.IsNullOrWhiteSpace(command.DigitalCode) && command.DigitalCode.Trim().Length > 3)
                errors.Add("DigitalCode must be 3 characters or less.");

            if (!string.IsNullOrWhiteSpace(command.CitizenshipName) && command.CitizenshipName.Trim().Length > 50)
                errors.Add("CitizenshipName must be 50 characters or less.");

            if (!string.IsNullOrWhiteSpace(command.CitizenshipNameEn) && command.CitizenshipNameEn.Trim().Length > 50)
                errors.Add("CitizenshipNameEn must be 50 characters or less.");

            return errors;
        }
    }
}