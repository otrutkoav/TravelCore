using System.Collections.Generic;
using System.Linq;
using TourCore.Application.Common.Errors;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Seating.VehiclePlans.Commands;

namespace TourCore.Application.Seating.VehiclePlans.Validators
{
    public class UpdateVehiclePlanCommandValidator
    {
        public void ValidateAndThrow(UpdateVehiclePlanCommand command)
        {
            var errors = Validate(command);

            if (errors.Count > 0)
                throw new ValidationException(errors);
        }

        public IReadOnlyDictionary<string, string[]> Validate(UpdateVehiclePlanCommand command)
        {
            var errors = new Dictionary<string, List<string>>();

            if (command == null)
            {
                AddError(errors, "General", ErrorCode.Required);
                return ToResult(errors);
            }

            if (command.Id <= 0)
                AddError(errors, "Id", ErrorCode.GreaterThanZero);

            if (command.TransportId <= 0)
                AddError(errors, "TransportId", ErrorCode.GreaterThanZero);

            if (command.RowsCount <= 0)
                AddError(errors, "RowsCount", ErrorCode.GreaterThanZero);

            if (command.ColumnsCount <= 0)
                AddError(errors, "ColumnsCount", ErrorCode.GreaterThanZero);

            if (command.AreaNumber <= 0)
                AddError(errors, "AreaNumber", ErrorCode.GreaterThanZero);

            if (!string.IsNullOrWhiteSpace(command.Name) && command.Name.Trim().Length > 20)
                AddError(errors, "Name", ErrorCode.MaxLength);

            if (!string.IsNullOrWhiteSpace(command.Comment) && command.Comment.Trim().Length > 250)
                AddError(errors, "Comment", ErrorCode.MaxLength);

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
            return errors.ToDictionary(x => x.Key, x => x.Value.ToArray());
        }
    }
}