using System.Collections.Generic;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Transfers.Commands;

namespace TourCore.Application.Transfers.Validators
{
    public class UpdateTransferCommandValidator
    {
        public void ValidateAndThrow(UpdateTransferCommand command)
        {
            var errors = Validate(command);
            if (errors.Count > 0)
                throw new ValidationException(errors);
        }

        public IReadOnlyCollection<string> Validate(UpdateTransferCommand command)
        {
            var errors = new List<string>();

            if (command == null)
            {
                errors.Add("Command is required.");
                return errors;
            }

            if (command.Id <= 0)
                errors.Add("Id must be greater than 0.");

            if (string.IsNullOrWhiteSpace(command.Name))
                errors.Add("Name is required.");
            else if (command.Name.Trim().Length > 100)
                errors.Add("Name must be 100 characters or less.");

            if (!string.IsNullOrWhiteSpace(command.NameEn) && command.NameEn.Trim().Length > 100)
                errors.Add("NameEn must be 100 characters or less.");

            if (!string.IsNullOrWhiteSpace(command.DurationText) && command.DurationText.Trim().Length > 5)
                errors.Add("DurationText must be 5 characters or less.");

            if (!string.IsNullOrWhiteSpace(command.PlaceFrom) && command.PlaceFrom.Trim().Length > 300)
                errors.Add("PlaceFrom must be 300 characters or less.");

            if (!string.IsNullOrWhiteSpace(command.PlaceTo) && command.PlaceTo.Trim().Length > 300)
                errors.Add("PlaceTo must be 300 characters or less.");

            if (command.CityId.HasValue && command.CityId.Value <= 0)
                errors.Add("CityId must be greater than 0.");

            if (command.DirectionId.HasValue && command.DirectionId.Value <= 0)
                errors.Add("DirectionId must be greater than 0.");

            if (!string.IsNullOrWhiteSpace(command.Url) && command.Url.Trim().Length > 192)
                errors.Add("Url must be 192 characters or less.");

            if (command.ShowOrder < 0)
                errors.Add("ShowOrder cannot be negative.");

            return errors;
        }
    }
}