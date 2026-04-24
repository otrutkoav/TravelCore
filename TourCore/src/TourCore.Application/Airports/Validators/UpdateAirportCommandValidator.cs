using System.Collections.Generic;
using TourCore.Application.Airports.Commands;
using TourCore.Application.Common.Exceptions;

namespace TourCore.Application.Airports.Validators
{
    public class UpdateAirportCommandValidator
    {
        public void ValidateAndThrow(UpdateAirportCommand command)
        {
            var errors = Validate(command);
            if (errors.Count > 0)
                throw new ValidationException(errors);
        }

        public IReadOnlyCollection<string> Validate(UpdateAirportCommand command)
        {
            var errors = new List<string>();

            if (command == null)
            {
                errors.Add("Command is required.");
                return errors;
            }

            if (command.Id <= 0)
                errors.Add("Id must be greater than 0.");

            if (command.CityId <= 0)
                errors.Add("CityId must be greater than 0.");

            if (string.IsNullOrWhiteSpace(command.Code))
                errors.Add("Code is required.");
            else if (command.Code.Trim().Length > 5)
                errors.Add("Code must be 5 characters or less.");

            if (string.IsNullOrWhiteSpace(command.Name))
                errors.Add("Name is required.");
            else if (command.Name.Trim().Length > 100)
                errors.Add("Name must be 100 characters or less.");

            if (!string.IsNullOrWhiteSpace(command.NameEn) && command.NameEn.Trim().Length > 100)
                errors.Add("NameEn must be 100 characters or less.");

            if (!string.IsNullOrWhiteSpace(command.IcaoCode) && command.IcaoCode.Trim().Length > 5)
                errors.Add("IcaoCode must be 5 characters or less.");

            if (!string.IsNullOrWhiteSpace(command.LetterCode) && command.LetterCode.Trim().Length > 1)
                errors.Add("LetterCode must be 1 character or less.");

            return errors;
        }
    }
}