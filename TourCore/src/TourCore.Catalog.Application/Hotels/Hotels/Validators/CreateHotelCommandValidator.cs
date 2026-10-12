using System.Collections.Generic;
using System.Linq;
using TourCore.Application.Common.Errors;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Hotels.Hotels.Commands;

namespace TourCore.Application.Hotels.Hotels.Validators
{
    public class CreateHotelCommandValidator
    {
        public void ValidateAndThrow(CreateHotelCommand command)
        {
            var errors = Validate(command);

            if (errors.Count > 0)
                throw new ValidationException(errors);
        }

        public IReadOnlyDictionary<string, string[]> Validate(CreateHotelCommand command)
        {
            var errors = new Dictionary<string, List<string>>();

            if (command == null)
            {
                AddError(errors, "General", ErrorCode.Required);
                return ToResult(errors);
            }

            if (command.CountryId <= 0)
                AddError(errors, "CountryId", ErrorCode.GreaterThanZero);

            if (command.CityId <= 0)
                AddError(errors, "CityId", ErrorCode.GreaterThanZero);

            if (command.ResortId.HasValue && command.ResortId.Value <= 0)
                AddError(errors, "ResortId", ErrorCode.GreaterThanZero);

            if (command.CategoryId.HasValue && command.CategoryId.Value <= 0)
                AddError(errors, "CategoryId", ErrorCode.GreaterThanZero);

            if (string.IsNullOrWhiteSpace(command.Name))
                AddError(errors, "Name", ErrorCode.Required);
            else if (command.Name.Trim().Length > 200)
                AddError(errors, "Name", ErrorCode.MaxLength);

            if (!string.IsNullOrWhiteSpace(command.NameEn) && command.NameEn.Trim().Length > 200)
                AddError(errors, "NameEn", ErrorCode.MaxLength);

            if (string.IsNullOrWhiteSpace(command.Stars))
                AddError(errors, "Stars", ErrorCode.Required);
            else if (command.Stars.Trim().Length > 20)
                AddError(errors, "Stars", ErrorCode.MaxLength);

            if (string.IsNullOrWhiteSpace(command.Code))
                AddError(errors, "Code", ErrorCode.Required);
            else if (command.Code.Trim().Length > 10)
                AddError(errors, "Code", ErrorCode.MaxLength);

            if (!string.IsNullOrWhiteSpace(command.Address) && command.Address.Trim().Length > 254)
                AddError(errors, "Address", ErrorCode.MaxLength);

            if (!string.IsNullOrWhiteSpace(command.Phone) && command.Phone.Trim().Length > 50)
                AddError(errors, "Phone", ErrorCode.MaxLength);

            if (!string.IsNullOrWhiteSpace(command.Fax) && command.Fax.Trim().Length > 20)
                AddError(errors, "Fax", ErrorCode.MaxLength);

            if (!string.IsNullOrWhiteSpace(command.Email) && command.Email.Trim().Length > 50)
                AddError(errors, "Email", ErrorCode.MaxLength);

            if (!string.IsNullOrWhiteSpace(command.Website) && command.Website.Trim().Length > 254)
                AddError(errors, "Website", ErrorCode.MaxLength);

            if (!string.IsNullOrWhiteSpace(command.Latitude) && command.Latitude.Trim().Length > 30)
                AddError(errors, "Latitude", ErrorCode.MaxLength);

            if (!string.IsNullOrWhiteSpace(command.Longitude) && command.Longitude.Trim().Length > 30)
                AddError(errors, "Longitude", ErrorCode.MaxLength);

            if (command.SortOrder < 0)
                AddError(errors, "SortOrder", ErrorCode.Negative);

            if (command.Rank.HasValue && command.Rank.Value < 0)
                AddError(errors, "Rank", ErrorCode.Negative);

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