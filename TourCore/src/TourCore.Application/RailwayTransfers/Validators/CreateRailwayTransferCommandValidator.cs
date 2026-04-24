using System.Collections.Generic;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.RailwayTransfers.Commands;

namespace TourCore.Application.RailwayTransfers.Validators
{
    public class CreateRailwayTransferCommandValidator
    {
        public void ValidateAndThrow(CreateRailwayTransferCommand command)
        {
            var errors = Validate(command);
            if (errors.Count > 0)
                throw new ValidationException(errors);
        }

        public IReadOnlyCollection<string> Validate(CreateRailwayTransferCommand command)
        {
            var errors = new List<string>();

            if (command == null)
            {
                errors.Add("Command is required.");
                return errors;
            }

            if (string.IsNullOrWhiteSpace(command.Name))
                errors.Add("Name is required.");

            if (command.CountryFromId <= 0)
                errors.Add("CountryFromId must be greater than 0.");

            if (command.CityFromId <= 0)
                errors.Add("CityFromId must be greater than 0.");

            if (command.CountryToId <= 0)
                errors.Add("CountryToId must be greater than 0.");

            if (command.CityToId <= 0)
                errors.Add("CityToId must be greater than 0.");

            if (command.CityFromId == command.CityToId)
                errors.Add("CityFromId and CityToId must be different.");

            return errors;
        }
    }
}