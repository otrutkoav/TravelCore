using System.Collections.Generic;
using System.Linq;
using TourCore.Application.Common.Errors;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Hotels.RoomCategories.Commands;

namespace TourCore.Application.Hotels.RoomCategories.Validators
{
    public class UpdateRoomCategoryCommandValidator
    {
        public void ValidateAndThrow(UpdateRoomCategoryCommand command)
        {
            var errors = Validate(command);

            if (errors.Count > 0)
                throw new ValidationException(errors);
        }

        public IReadOnlyDictionary<string, string[]> Validate(UpdateRoomCategoryCommand command)
        {
            var errors = new Dictionary<string, List<string>>();

            if (command == null)
            {
                AddError(errors, "General", ErrorCode.Required);
                return ToResult(errors);
            }

            if (command.Id <= 0)
                AddError(errors, "Id", ErrorCode.GreaterThanZero);

            if (string.IsNullOrWhiteSpace(command.Code))
                AddError(errors, "Code", ErrorCode.Required);
            else if (command.Code.Trim().Length > 40)
                AddError(errors, "Code", ErrorCode.MaxLength);

            if (string.IsNullOrWhiteSpace(command.Name))
                AddError(errors, "Name", ErrorCode.Required);
            else if (command.Name.Trim().Length > 150)
                AddError(errors, "Name", ErrorCode.MaxLength);

            if (!string.IsNullOrWhiteSpace(command.NameEn) && command.NameEn.Trim().Length > 100)
                AddError(errors, "NameEn", ErrorCode.MaxLength);

            if (command.SortOrder < 0)
                AddError(errors, "SortOrder", ErrorCode.Negative);

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