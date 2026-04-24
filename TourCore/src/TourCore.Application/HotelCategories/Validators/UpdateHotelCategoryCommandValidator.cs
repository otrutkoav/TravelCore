using System.Collections.Generic;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.HotelCategories.Commands;

namespace TourCore.Application.HotelCategories.Validators
{
    public class UpdateHotelCategoryCommandValidator
    {
        public void ValidateAndThrow(UpdateHotelCategoryCommand command)
        {
            var errors = Validate(command);
            if (errors.Count > 0)
                throw new ValidationException(errors);
        }

        public IReadOnlyCollection<string> Validate(UpdateHotelCategoryCommand command)
        {
            var errors = new List<string>();

            if (command == null)
            {
                errors.Add("Command is required.");
                return errors;
            }

            if (command.Id <= 0)
                errors.Add("Id must be greater than 0.");

            var createValidator = new CreateHotelCategoryCommandValidator();
            foreach (var error in createValidator.Validate(command))
                errors.Add(error);

            return errors;
        }
    }
}