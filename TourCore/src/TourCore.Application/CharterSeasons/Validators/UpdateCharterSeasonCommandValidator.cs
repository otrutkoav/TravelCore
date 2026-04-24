using System.Collections.Generic;
using TourCore.Application.CharterSeasons.Commands;
using TourCore.Application.Common.Exceptions;

namespace TourCore.Application.CharterSeasons.Validators
{
    public class UpdateCharterSeasonCommandValidator
    {
        public void ValidateAndThrow(UpdateCharterSeasonCommand command)
        {
            var errors = Validate(command);
            if (errors.Count > 0)
                throw new ValidationException(errors);
        }

        public IReadOnlyCollection<string> Validate(UpdateCharterSeasonCommand command)
        {
            var errors = new List<string>();

            if (command == null)
            {
                errors.Add("Command is required.");
                return errors;
            }

            if (command.Id <= 0)
                errors.Add("Id must be greater than 0.");

            var createValidator = new CreateCharterSeasonCommandValidator();
            foreach (var error in createValidator.Validate(command))
                errors.Add(error);

            return errors;
        }
    }
}