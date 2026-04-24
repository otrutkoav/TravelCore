using System.Collections.Generic;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.HotelRoomCombinations.Commands;

namespace TourCore.Application.HotelRoomCombinations.Validators
{
    public class UpdateHotelRoomCombinationCommandValidator
    {
        public void ValidateAndThrow(UpdateHotelRoomCombinationCommand command)
        {
            var errors = Validate(command);
            if (errors.Count > 0)
                throw new ValidationException(errors);
        }

        public IReadOnlyCollection<string> Validate(UpdateHotelRoomCombinationCommand command)
        {
            var errors = new List<string>();

            if (command == null)
            {
                errors.Add("Command is required.");
                return errors;
            }

            if (command.Id <= 0)
                errors.Add("Id must be greater than 0.");

            var createValidator = new CreateHotelRoomCombinationCommandValidator();
            foreach (var error in createValidator.Validate(command))
                errors.Add(error);

            return errors;
        }
    }
}