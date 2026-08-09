using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Abstractions.Persistence.Tours;
using TourCore.Application.Abstractions.Services;
using TourCore.Application.Common.Errors;
using TourCore.Application.Common.Exceptions;
using TourCore.Contracts.Tours;
using TourCore.Domain.Services.Enums;
using TourCore.Domain.Tours;

namespace TourCore.Application.Tours
{
    public class ReplaceTourFlightsHandler
        : ICommandHandler<ReplaceTourFlightsCommand, IReadOnlyCollection<TourFlightGroupDto>>
    {
        private readonly ITourRepository _tours;
        private readonly ITourServiceRepository _services;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _clock;
        private readonly TourFlightCommandValidator _validator;

        public ReplaceTourFlightsHandler(
            ITourRepository tours,
            ITourServiceRepository services,
            IUnitOfWork unitOfWork,
            IDateTimeProvider clock,
            TourFlightCommandValidator validator
        )
        {
            _tours = tours;
            _services = services;
            _unitOfWork = unitOfWork;
            _clock = clock;
            _validator = validator;
        }

        public async Task<IReadOnlyCollection<TourFlightGroupDto>> Handle(
            ReplaceTourFlightsCommand command,
            CancellationToken cancellationToken
        )
        {
            _validator.ValidateAndThrow(command);

            var tour = await _tours.GetByIdAsync(command.TourId, cancellationToken);
            if (tour == null)
                throw new NotFoundException(ErrorMessages.TourNotFound, ErrorCode.TourNotFound);

            var service = await _services.GetWithFlightsAsync(
                command.TourId,
                command.TourServiceId,
                cancellationToken
            );

            if (service == null)
                throw new NotFoundException(
                    ErrorMessages.TourServiceNotFound,
                    ErrorCode.TourServiceNotFound
                );

            if (service.ServiceTypeId != (int)ServiceTypeEnum.Flight)
                throw new ValidationException(
                    new[] { "Flight settings are allowed only for the Flight service type." }
                );

            var inputs = (command.Groups ?? new List<TourFlightGroupInput>()).ToArray();
            var now = _clock.UtcNow;
            var groups = new List<TourFlightGroup>();

            foreach (var input in inputs.OrderBy(x => x.OrderIndex))
            {
                var flightInputs = (input.Flights ?? new List<TourFlightInput>()).ToArray();
                var group = new TourFlightGroup(
                    service.Id,
                    input.Name,
                    input.Priority,
                    input.OrderIndex,
                    now
                );

                foreach (var inputFlight in flightInputs.OrderBy(x => x.OrderIndex))
                {
                    group.Flights.Add(CreateFlight(inputFlight, now));
                }

                groups.Add(group);
            }

            _services.ReplaceFlightGroups(service, groups);
            service.IncrementRevision(now);
            tour.IncrementRevision();
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return groups.OrderBy(x => x.OrderIndex).Select(TourFlightMapping.ToDto).ToArray();
        }

        private static TourFlight CreateFlight(TourFlightInput input, DateTime createdAt)
        {
            return new TourFlight(
                0,
                input.OrderIndex,
                TourFlightMapping.Pack(input.DepartureCountryIds),
                TourFlightMapping.Pack(input.DepartureCityIds),
                TourFlightMapping.Pack(input.ArrivalCountryIds),
                TourFlightMapping.Pack(input.ArrivalCityIds),
                TourFlightMapping.Pack(input.AirlineIds),
                TourFlightMapping.Pack(input.FlightIds),
                TourFlightMapping.Pack(input.DepartureAirportIds),
                TourFlightMapping.Pack(input.ArrivalAirportIds),
                TourFlightMapping.Pack(input.FareIds),
                input.MinConnectionMinutes,
                input.MaxConnectionMinutes,
                createdAt
            );
        }
    }

    public class GetTourFlightsHandler
        : IQueryHandler<GetTourFlightsQuery, IReadOnlyCollection<TourFlightGroupDto>>
    {
        private readonly ITourServiceRepository _services;

        public GetTourFlightsHandler(ITourServiceRepository services)
        {
            _services = services;
        }

        public async Task<IReadOnlyCollection<TourFlightGroupDto>> Handle(
            GetTourFlightsQuery query,
            CancellationToken cancellationToken
        )
        {
            var service = await _services.GetWithFlightsAsync(
                query.TourId,
                query.TourServiceId,
                cancellationToken
            );

            if (service == null)
                throw new NotFoundException(
                    ErrorMessages.TourServiceNotFound,
                    ErrorCode.TourServiceNotFound
                );

            return service
                .FlightGroups.OrderBy(x => x.OrderIndex)
                .Select(TourFlightMapping.ToDto)
                .ToArray();
        }
    }
}
