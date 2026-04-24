using System.Collections.Generic;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.RoomCategories.Commands;

namespace TourCore.Application.RoomCategories.Validators
{
    public class UpdateRoomCategoryCommandValidator
    {
        public void ValidateAndThrow(UpdateRoomCategoryCommand command)
        {
            var errors = Validate(command);
            if (errors.Count > 0)
                throw new ValidationException(errors);
        }

        public IReadOnlyCollection<string> Validate(UpdateRoomCategoryCommand command)
        {
            var errors = new List<string>();

            if (command == null)
            {
                errors.Add("Command is required.");
                return errors;
            }

            if (command.Id <= 0)
                errors.Add("Id must be greater than 0.");

            var createValidator = new CreateRoomCategoryCommandValidator();
            foreach (var error in createValidator.Validate(command))
                errors.Add(error);

            return errors;
        }
    }
}