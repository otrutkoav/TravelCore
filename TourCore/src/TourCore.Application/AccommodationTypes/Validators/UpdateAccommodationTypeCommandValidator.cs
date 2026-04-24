using System.Collections.Generic;
using TourCore.Application.AccommodationTypes.Commands;
using TourCore.Application.Common.Exceptions;

namespace TourCore.Application.AccommodationTypes.Validators
{
    public class UpdateAccommodationTypeCommandValidator
    {
        public void ValidateAndThrow(UpdateAccommodationTypeCommand command)
        {
            var errors = Validate(command);
            if (errors.Count > 0)
                throw new ValidationException(errors);
        }

        public IReadOnlyCollection<string> Validate(UpdateAccommodationTypeCommand command)
        {
            var errors = new List<string>();

            if (command == null)
            {
                errors.Add("Command is required.");
                return errors;
            }

            if (command.Id <= 0)
                errors.Add("Id must be greater than 0.");

            var createValidator = new CreateAccommodationTypeCommandValidator();
            foreach (var error in createValidator.Validate(command))
                errors.Add(error);

            return errors;
        }
    }
}