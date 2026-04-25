using System.Collections.Generic;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Hotels.Commands;

namespace TourCore.Application.Hotels.Validators
{
    public class UpdateHotelCommandValidator
    {
        private readonly CreateHotelCommandValidator _createValidator;

        public UpdateHotelCommandValidator()
        {
            _createValidator = new CreateHotelCommandValidator();
        }

        public void ValidateAndThrow(UpdateHotelCommand command)
        {
            var errors = Validate(command);
            if (errors.Count > 0)
                throw new ValidationException(errors);
        }

        public IReadOnlyCollection<string> Validate(UpdateHotelCommand command)
        {
            var errors = new List<string>();

            if (command == null)
            {
                errors.Add("Command is required.");
                return errors;
            }

            if (command.Id <= 0)
                errors.Add("Id is required.");

            foreach (var error in _createValidator.Validate(command))
                errors.Add(error);

            return errors;
        }
    }
}