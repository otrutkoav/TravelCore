using System.Collections.Generic;
using System.Linq;
using TourCore.Application.Avia.CharterSeasons.Commands;
using TourCore.Application.Common.Errors;
using TourCore.Application.Common.Exceptions;

namespace TourCore.Application.Avia.CharterSeasons.Validators
{
    public class CreateCharterSeasonCommandValidator
    {
        public void ValidateAndThrow(CreateCharterSeasonCommand command)
        {
            var errors = Validate(command);

            if (errors.Count > 0)
                throw new ValidationException(errors);
        }

        public IReadOnlyDictionary<string, string[]> Validate(CreateCharterSeasonCommand command)
        {
            var errors = new Dictionary<string, List<string>>();

            if (command == null)
            {
                AddError(errors, "General", ErrorCode.Required);
                return ToResult(errors);
            }

            ValidateCommon(
                command.CharterId,
                command.DateFrom,
                command.DateTo,
                command.Remark,
                errors);

            return ToResult(errors);
        }

        internal static void ValidateCommon(
            int charterId,
            System.DateTime? dateFrom,
            System.DateTime? dateTo,
            string remark,
            IDictionary<string, List<string>> errors)
        {
            if (charterId <= 0)
                AddError(errors, "CharterId", ErrorCode.GreaterThanZero);

            if (dateFrom.HasValue &&
                dateTo.HasValue &&
                dateFrom.Value.Date > dateTo.Value.Date)
            {
                AddError(errors, "DateFrom", ErrorCode.DateRangeInvalid);
            }

            if (!string.IsNullOrWhiteSpace(remark) &&
                remark.Trim().Length > 20)
            {
                AddError(errors, "Remark", ErrorCode.MaxLength);
            }
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