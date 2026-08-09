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
    public class ReplaceTourHotelsHandler
        : ICommandHandler<ReplaceTourHotelsCommand, IReadOnlyCollection<TourHotelGroupDto>>
    {
        private readonly ITourRepository _tours;
        private readonly ITourServiceRepository _services;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _clock;
        private readonly TourHotelCommandValidator _validator;

        public ReplaceTourHotelsHandler(
            ITourRepository tours,
            ITourServiceRepository services,
            IUnitOfWork unitOfWork,
            IDateTimeProvider clock,
            TourHotelCommandValidator validator
        )
        {
            _tours = tours;
            _services = services;
            _unitOfWork = unitOfWork;
            _clock = clock;
            _validator = validator;
        }

        public async Task<IReadOnlyCollection<TourHotelGroupDto>> Handle(
            ReplaceTourHotelsCommand command,
            CancellationToken cancellationToken
        )
        {
            _validator.ValidateAndThrow(command);

            var tour = await _tours.GetByIdAsync(command.TourId, cancellationToken);
            if (tour == null)
                throw new NotFoundException(ErrorMessages.TourNotFound, ErrorCode.TourNotFound);

            var service = await _services.GetWithHotelsAsync(
                command.TourId,
                command.TourServiceId,
                cancellationToken
            );
            if (service == null)
                throw new NotFoundException(
                    ErrorMessages.TourServiceNotFound,
                    ErrorCode.TourServiceNotFound
                );

            if (service.ServiceTypeId != (int)ServiceTypeEnum.Hotel)
                throw new ValidationException(
                    new[] { "Hotel settings are allowed only for the Hotel service type." }
                );

            var now = _clock.UtcNow;
            var groups = new List<TourHotelGroup>();

            foreach (
                var input in (command.Groups ?? new List<TourHotelGroupInput>()).OrderBy(x =>
                    x.OrderIndex
                )
            )
            {
                var group = new TourHotelGroup(
                    service.Id,
                    input.Name,
                    input.Priority,
                    input.OrderIndex,
                    now
                );

                foreach (var hotelInput in input.Hotels.OrderBy(x => x.OrderIndex))
                {
                    var hotel = new TourHotel(0, hotelInput.OrderIndex, hotelInput.CountryId, now);
                    hotel.AddCities(hotelInput.CityIds);
                    hotel.AddDurations(hotelInput.DurationsInNights);
                    hotel.AddAllowedHotels(hotelInput.HotelIds);
                    group.Hotels.Add(hotel);
                }

                groups.Add(group);
            }

            _services.ReplaceHotelGroups(service, groups);
            service.IncrementRevision(now);
            tour.IncrementRevision();
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return groups.OrderBy(x => x.OrderIndex).Select(TourHotelMapping.ToDto).ToArray();
        }
    }

    public class GetTourHotelsHandler
        : IQueryHandler<GetTourHotelsQuery, IReadOnlyCollection<TourHotelGroupDto>>
    {
        private readonly ITourServiceRepository _services;

        public GetTourHotelsHandler(ITourServiceRepository services)
        {
            _services = services;
        }

        public async Task<IReadOnlyCollection<TourHotelGroupDto>> Handle(
            GetTourHotelsQuery query,
            CancellationToken cancellationToken
        )
        {
            var service = await _services.GetWithHotelsAsync(
                query.TourId,
                query.TourServiceId,
                cancellationToken
            );
            if (service == null)
                throw new NotFoundException(
                    ErrorMessages.TourServiceNotFound,
                    ErrorCode.TourServiceNotFound
                );

            if (service.ServiceTypeId != (int)ServiceTypeEnum.Hotel)
                throw new ValidationException(
                    new[] { "Hotel settings are allowed only for the Hotel service type." }
                );

            return service
                .HotelGroups.OrderBy(x => x.OrderIndex)
                .Select(TourHotelMapping.ToDto)
                .ToArray();
        }
    }
}
