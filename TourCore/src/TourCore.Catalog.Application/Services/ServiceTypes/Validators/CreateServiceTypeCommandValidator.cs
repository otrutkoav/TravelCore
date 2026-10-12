using System.Collections.Generic;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Services.ServiceTypes.Commands;

namespace TourCore.Application.Services.ServiceTypes.Validators
{
    public class CreateServiceTypeCommandValidator
    {
        public void ValidateAndThrow(CreateServiceTypeCommand command)
        {
            var errors = Validate(command);
            if (errors.Count > 0)
                throw new ValidationException(errors);
        }

        public IReadOnlyCollection<string> Validate(CreateServiceTypeCommand command)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(command.Name))
                errors.Add("Name is required.");
            else if (command.Name.Trim().Length > 50)
                errors.Add("Name must be 50 characters or less.");

            if (!string.IsNullOrWhiteSpace(command.NameEn) && command.NameEn.Trim().Length > 50)
                errors.Add("NameEn must be 50 characters or less.");

            if (!string.IsNullOrWhiteSpace(command.Code) && command.Code.Trim().Length > 10)
                errors.Add("Code must be 10 characters or less.");

            return errors;
        }
    }
}
