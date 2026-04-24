using System.Collections.Generic;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.HotelCategories.Commands;

namespace TourCore.Application.HotelCategories.Validators
{
    public class CreateHotelCategoryCommandValidator
    {
        public void ValidateAndThrow(CreateHotelCategoryCommand command)
        {
            var errors = Validate(command);
            if (errors.Count > 0)
                throw new ValidationException(errors);
        }

        public IReadOnlyCollection<string> Validate(CreateHotelCategoryCommand command)
        {
            var errors = new List<string>();

            if (command == null)
            {
                errors.Add("Command is required.");
                return errors;
            }

            if (string.IsNullOrWhiteSpace(command.Name))
                errors.Add("Name is required.");
            else if (command.Name.Trim().Length > 50)
                errors.Add("Name must be 50 characters or less.");

            if (!string.IsNullOrWhiteSpace(command.NameEn) && command.NameEn.Trim().Length > 50)
                errors.Add("NameEn must be 50 characters or less.");

            if (command.PrintOrder.HasValue && command.PrintOrder.Value < 0)
                errors.Add("PrintOrder cannot be negative.");

            if (!string.IsNullOrWhiteSpace(command.GlobalCode) && command.GlobalCode.Trim().Length > 20)
                errors.Add("GlobalCode must be 20 characters or less.");

            return errors;
        }
    }
}