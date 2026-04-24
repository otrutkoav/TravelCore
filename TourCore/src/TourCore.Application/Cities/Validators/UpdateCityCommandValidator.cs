using System.Collections.Generic;
using TourCore.Application.Cities.Commands;
using TourCore.Application.Common.Exceptions;

namespace TourCore.Application.Cities.Validators
{
    public class UpdateCityCommandValidator
    {
        public void ValidateAndThrow(UpdateCityCommand command)
        {
            var errors = Validate(command);
            if (errors.Count > 0)
                throw new ValidationException(errors);
        }

        public IReadOnlyCollection<string> Validate(UpdateCityCommand command)
        {
            var errors = new List<string>();

            if (command == null)
            {
                errors.Add("Command is required.");
                return errors;
            }

            if (command.Id <= 0)
                errors.Add("Id must be greater than 0.");

            if (command.CountryId <= 0)
                errors.Add("CountryId must be greater than 0.");

            if (command.RegionId.HasValue && command.RegionId.Value <= 0)
                errors.Add("RegionId must be greater than 0.");

            if (string.IsNullOrWhiteSpace(command.Name))
                errors.Add("Name is required.");
            else if (command.Name.Trim().Length > 100)
                errors.Add("Name must be 100 characters or less.");

            if (!string.IsNullOrWhiteSpace(command.NameEn) && command.NameEn.Trim().Length > 100)
                errors.Add("NameEn must be 100 characters or less.");

            if (string.IsNullOrWhiteSpace(command.Code))
                errors.Add("Code is required.");
            else if (command.Code.Trim().Length > 3)
                errors.Add("Code must be 3 characters or less.");

            if (command.SortOrder < 0)
                errors.Add("SortOrder cannot be negative.");

            if (!string.IsNullOrWhiteSpace(command.TimeZone) && command.TimeZone.Trim().Length > 150)
                errors.Add("TimeZone must be 150 characters or less.");

            if (!string.IsNullOrWhiteSpace(command.IataCode) && command.IataCode.Trim().Length > 3)
                errors.Add("IataCode must be 3 characters or less.");

            if (!string.IsNullOrWhiteSpace(command.Coordinates) && command.Coordinates.Trim().Length > 30)
                errors.Add("Coordinates must be 30 characters or less.");

            return errors;
        }
    }
}