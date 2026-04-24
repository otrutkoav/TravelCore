using System.Collections.Generic;
using TourCore.Application.AccommodationTypes.Commands;
using TourCore.Application.Common.Exceptions;

namespace TourCore.Application.AccommodationTypes.Validators
{
    public class CreateAccommodationTypeCommandValidator
    {
        public void ValidateAndThrow(CreateAccommodationTypeCommand command)
        {
            var errors = Validate(command);
            if (errors.Count > 0)
                throw new ValidationException(errors);
        }

        public IReadOnlyCollection<string> Validate(CreateAccommodationTypeCommand command)
        {
            var errors = new List<string>();

            if (command == null)
            {
                errors.Add("Command is required.");
                return errors;
            }

            if (string.IsNullOrWhiteSpace(command.Code))
                errors.Add("Code is required.");
            else if (command.Code.Trim().Length > 50)
                errors.Add("Code must be 50 characters or less.");

            if (string.IsNullOrWhiteSpace(command.Name))
                errors.Add("Name is required.");
            else if (command.Name.Trim().Length > 100)
                errors.Add("Name must be 100 characters or less.");

            if (!string.IsNullOrWhiteSpace(command.NameEn) && command.NameEn.Trim().Length > 100)
                errors.Add("NameEn must be 100 characters or less.");

            if (command.AgeFrom.HasValue && command.AgeFrom.Value < 0)
                errors.Add("AgeFrom cannot be negative.");

            if (command.AgeTo.HasValue && command.AgeTo.Value < 0)
                errors.Add("AgeTo cannot be negative.");

            if (command.AgeFrom.HasValue && command.AgeTo.HasValue && command.AgeFrom.Value > command.AgeTo.Value)
                errors.Add("AgeFrom cannot be greater than AgeTo.");

            if (command.PerRoom.HasValue && command.PerRoom.Value < 0)
                errors.Add("PerRoom cannot be negative.");

            if (command.SortOrder < 0)
                errors.Add("SortOrder cannot be negative.");

            return errors;
        }
    }
}