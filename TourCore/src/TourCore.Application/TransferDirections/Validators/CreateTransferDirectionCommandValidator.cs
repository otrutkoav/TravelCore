using System.Collections.Generic;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.TransferDirections.Commands;

namespace TourCore.Application.TransferDirections.Validators
{
    public class CreateTransferDirectionCommandValidator
    {
        public void ValidateAndThrow(CreateTransferDirectionCommand command)
        {
            var errors = Validate(command);
            if (errors.Count > 0)
                throw new ValidationException(errors);
        }

        public IReadOnlyCollection<string> Validate(CreateTransferDirectionCommand command)
        {
            var errors = new List<string>();

            if (command == null)
            {
                errors.Add("Command is required.");
                return errors;
            }

            if (string.IsNullOrWhiteSpace(command.Name))
                errors.Add("Name is required.");
            else if (command.Name.Trim().Length > 128)
                errors.Add("Name must be 128 characters or less.");

            return errors;
        }
    }
}