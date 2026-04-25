using System.Collections.Generic;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Hotels.Commands;

namespace TourCore.Application.Hotels.Validators
{
    public class CreateHotelCommandValidator
    {
        public void ValidateAndThrow(CreateHotelCommand command)
        {
            var errors = Validate(command);
            if (errors.Count > 0)
                throw new ValidationException(errors);
        }

        public IReadOnlyCollection<string> Validate(CreateHotelCommand command)
        {
            var errors = new List<string>();

            if (command == null)
            {
                errors.Add("Command is required.");
                return errors;
            }

            if (command.CountryId <= 0)
                errors.Add("CountryId is required.");

            if (command.CityId <= 0)
                errors.Add("CityId is required.");

            if (command.ResortId.HasValue && command.ResortId.Value <= 0)
                errors.Add("ResortId must be greater than zero.");

            if (command.CategoryId.HasValue && command.CategoryId.Value <= 0)
                errors.Add("CategoryId must be greater than zero.");

            if (string.IsNullOrWhiteSpace(command.Name))
                errors.Add("Name is required.");
            else if (command.Name.Trim().Length > 200)
                errors.Add("Name must be 200 characters or less.");

            if (!string.IsNullOrWhiteSpace(command.NameEn) && command.NameEn.Trim().Length > 200)
                errors.Add("NameEn must be 200 characters or less.");

            if (string.IsNullOrWhiteSpace(command.Stars))
                errors.Add("Stars is required.");
            else if (command.Stars.Trim().Length > 20)
                errors.Add("Stars must be 20 characters or less.");

            if (string.IsNullOrWhiteSpace(command.Code))
                errors.Add("Code is required.");
            else if (command.Code.Trim().Length > 10)
                errors.Add("Code must be 10 characters or less.");

            if (!string.IsNullOrWhiteSpace(command.Address) && command.Address.Trim().Length > 254)
                errors.Add("Address must be 254 characters or less.");

            if (!string.IsNullOrWhiteSpace(command.Phone) && command.Phone.Trim().Length > 50)
                errors.Add("Phone must be 50 characters or less.");

            if (!string.IsNullOrWhiteSpace(command.Fax) && command.Fax.Trim().Length > 20)
                errors.Add("Fax must be 20 characters or less.");

            if (!string.IsNullOrWhiteSpace(command.Email) && command.Email.Trim().Length > 50)
                errors.Add("Email must be 50 characters or less.");

            if (!string.IsNullOrWhiteSpace(command.Website) && command.Website.Trim().Length > 254)
                errors.Add("Website must be 254 characters or less.");

            if (!string.IsNullOrWhiteSpace(command.Latitude) && command.Latitude.Trim().Length > 30)
                errors.Add("Latitude must be 30 characters or less.");

            if (!string.IsNullOrWhiteSpace(command.Longitude) && command.Longitude.Trim().Length > 30)
                errors.Add("Longitude must be 30 characters or less.");

            if (command.SortOrder < 0)
                errors.Add("SortOrder cannot be negative.");

            if (command.Rank.HasValue && command.Rank.Value < 0)
                errors.Add("Rank cannot be negative.");

            return errors;
        }
    }
}