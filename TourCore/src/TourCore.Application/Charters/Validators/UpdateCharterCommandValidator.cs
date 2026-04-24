using System.Collections.Generic;
using TourCore.Application.Charters.Commands;
using TourCore.Application.Common.Exceptions;

namespace TourCore.Application.Charters.Validators
{
    public class UpdateCharterCommandValidator
    {
        public void ValidateAndThrow(UpdateCharterCommand command)
        {
            var errors = Validate(command);
            if (errors.Count > 0)
                throw new ValidationException(errors);
        }

        public IReadOnlyCollection<string> Validate(UpdateCharterCommand command)
        {
            var errors = new List<string>();

            if (command == null)
            {
                errors.Add("Command is required.");
                return errors;
            }

            if (command.Id <= 0)
                errors.Add("Id must be greater than 0.");

            var createValidator = new CreateCharterCommandValidator();
            foreach (var error in createValidator.Validate(command))
                errors.Add(error);

            return errors;
        }
    }
}