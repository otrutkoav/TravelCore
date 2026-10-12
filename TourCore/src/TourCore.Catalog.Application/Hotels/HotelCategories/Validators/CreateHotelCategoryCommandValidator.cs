using System.Collections.Generic;
using System.Linq;
using TourCore.Application.Common.Errors;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Hotels.HotelCategories.Commands;

namespace TourCore.Application.Hotels.HotelCategories.Validators
{
    public class CreateHotelCategoryCommandValidator
    {
        public void ValidateAndThrow(CreateHotelCategoryCommand command)
        {
            var errors = Validate(command);

            if (errors.Count > 0)
                throw new ValidationException(errors);
        }

        public IReadOnlyDictionary<string, string[]> Validate(CreateHotelCategoryCommand command)
        {
            var errors = new Dictionary<string, List<string>>();

            if (command == null)
            {
                AddError(errors, "General", ErrorCode.Required);
                return ToResult(errors);
            }

            if (string.IsNullOrWhiteSpace(command.Name))
                AddError(errors, "Name", ErrorCode.Required);
            else if (command.Name.Trim().Length > 50)
                AddError(errors, "Name", ErrorCode.MaxLength);

            if (!string.IsNullOrWhiteSpace(command.NameEn) && command.NameEn.Trim().Length > 50)
                AddError(errors, "NameEn", ErrorCode.MaxLength);

            if (command.PrintOrder.HasValue && command.PrintOrder.Value < 0)
                AddError(errors, "PrintOrder", ErrorCode.Negative);

            if (!string.IsNullOrWhiteSpace(command.GlobalCode) && command.GlobalCode.Trim().Length > 20)
                AddError(errors, "GlobalCode", ErrorCode.MaxLength);

            return ToResult(errors);
        }

        private static void AddError(
            IDictionary<string, List<string>> errors,
            string field,
            string code)
        {
            if (!errors.ContainsKey(field))
                errors[field] = new List<string>();

            errors[field].Add(code);
        }

        private static IReadOnlyDictionary<string, string[]> ToResult(
            IDictionary<string, List<string>> errors)
        {
            return errors.ToDictionary(
                x => x.Key,
                x => x.Value.ToArray());
        }
    }
}