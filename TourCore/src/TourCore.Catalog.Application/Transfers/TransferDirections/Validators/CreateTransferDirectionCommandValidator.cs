using System.Collections.Generic;
using System.Linq;
using TourCore.Application.Common.Errors;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Transfers.TransferDirections.Commands;

namespace TourCore.Application.Transfers.TransferDirections.Validators
{
    public class CreateTransferDirectionCommandValidator
    {
        public void ValidateAndThrow(CreateTransferDirectionCommand command)
        {
            var errors = Validate(command);

            if (errors.Count > 0)
                throw new ValidationException(errors);
        }

        public IReadOnlyDictionary<string, string[]> Validate(CreateTransferDirectionCommand command)
        {
            var errors = new Dictionary<string, List<string>>();

            if (command == null)
            {
                AddError(errors, "General", ErrorCode.Required);
                return ToResult(errors);
            }

            if (string.IsNullOrWhiteSpace(command.Name))
                AddError(errors, "Name", ErrorCode.Required);
            else if (command.Name.Trim().Length > 128)
                AddError(errors, "Name", ErrorCode.MaxLength);

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