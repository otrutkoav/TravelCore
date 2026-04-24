using System.Collections.Generic;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.RoomTypes.Commands;

namespace TourCore.Application.RoomTypes.Validators
{
    public class CreateRoomTypeCommandValidator
    {
        public void ValidateAndThrow(CreateRoomTypeCommand command)
        {
            var errors = Validate(command);
            if (errors.Count > 0)
                throw new ValidationException(errors);
        }

        public IReadOnlyCollection<string> Validate(CreateRoomTypeCommand command)
        {
            var errors = new List<string>();

            if (command == null)
            {
                errors.Add("Command is required.");
                return errors;
            }

            if (string.IsNullOrWhiteSpace(command.Code))
                errors.Add("Code is required.");
            else if (command.Code.Trim().Length > 25)
                errors.Add("Code must be 25 characters or less.");

            if (string.IsNullOrWhiteSpace(command.Name))
                errors.Add("Name is required.");
            else if (command.Name.Trim().Length > 100)
                errors.Add("Name must be 100 characters or less.");

            if (!string.IsNullOrWhiteSpace(command.NameEn) && command.NameEn.Trim().Length > 100)
                errors.Add("NameEn must be 100 characters or less.");

            if (command.Places.HasValue && command.Places.Value < 0)
                errors.Add("Places cannot be negative.");

            if (command.ExtraPlaces.HasValue && command.ExtraPlaces.Value < 0)
                errors.Add("ExtraPlaces cannot be negative.");

            if (command.SortOrder < 0)
                errors.Add("SortOrder cannot be negative.");

            return errors;
        }
    }
}