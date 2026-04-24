using System.Collections.Generic;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.RoomCategories.Commands;

namespace TourCore.Application.RoomCategories.Validators
{
    public class CreateRoomCategoryCommandValidator
    {
        public void ValidateAndThrow(CreateRoomCategoryCommand command)
        {
            var errors = Validate(command);
            if (errors.Count > 0)
                throw new ValidationException(errors);
        }

        public IReadOnlyCollection<string> Validate(CreateRoomCategoryCommand command)
        {
            var errors = new List<string>();

            if (command == null)
            {
                errors.Add("Command is required.");
                return errors;
            }

            if (string.IsNullOrWhiteSpace(command.Code))
                errors.Add("Code is required.");
            else if (command.Code.Trim().Length > 40)
                errors.Add("Code must be 40 characters or less.");

            if (string.IsNullOrWhiteSpace(command.Name))
                errors.Add("Name is required.");
            else if (command.Name.Trim().Length > 150)
                errors.Add("Name must be 150 characters or less.");

            if (!string.IsNullOrWhiteSpace(command.NameEn) && command.NameEn.Trim().Length > 100)
                errors.Add("NameEn must be 100 characters or less.");

            if (command.SortOrder < 0)
                errors.Add("SortOrder cannot be negative.");

            return errors;
        }
    }
}