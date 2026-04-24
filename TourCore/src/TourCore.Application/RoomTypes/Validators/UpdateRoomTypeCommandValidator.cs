using System.Collections.Generic;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.RoomTypes.Commands;

namespace TourCore.Application.RoomTypes.Validators
{
    public class UpdateRoomTypeCommandValidator
    {
        public void ValidateAndThrow(UpdateRoomTypeCommand command)
        {
            var errors = Validate(command);
            if (errors.Count > 0)
                throw new ValidationException(errors);
        }

        public IReadOnlyCollection<string> Validate(UpdateRoomTypeCommand command)
        {
            var errors = new List<string>();

            if (command == null)
            {
                errors.Add("Command is required.");
                return errors;
            }

            if (command.Id <= 0)
                errors.Add("Id must be greater than 0.");

            var createValidator = new CreateRoomTypeCommandValidator();
            foreach (var error in createValidator.Validate(command))
                errors.Add(error);

            return errors;
        }
    }
}