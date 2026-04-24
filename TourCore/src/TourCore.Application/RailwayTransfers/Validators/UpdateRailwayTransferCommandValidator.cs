using System.Collections.Generic;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.RailwayTransfers.Commands;

namespace TourCore.Application.RailwayTransfers.Validators
{
    public class UpdateRailwayTransferCommandValidator
    {
        public void ValidateAndThrow(UpdateRailwayTransferCommand command)
        {
            var errors = Validate(command);
            if (errors.Count > 0)
                throw new ValidationException(errors);
        }

        public IReadOnlyCollection<string> Validate(UpdateRailwayTransferCommand command)
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

            if (command.CountryFromId <= 0)
                errors.Add("CountryFromId must be greater than 0.");

            if (command.CityFromId <= 0)
                errors.Add("CityFromId must be greater than 0.");

            if (command.CountryToId <= 0)
                errors.Add("CountryToId must be greater than 0.");

            if (command.CityToId <= 0)
                errors.Add("CityToId must be greater than 0.");

            if (command.CityFromId > 0 &&
                command.CityToId > 0 &&
                command.CityFromId == command.CityToId)
            {
                errors.Add("CityFromId and CityToId must be different.");
            }

            return errors;
        }
    }
}