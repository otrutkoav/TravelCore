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
    public class ReplaceTourBusTransferHandler
        : ICommandHandler<ReplaceTourBusTransferCommand, TourBusTransferDto>
    {
        private readonly ITourRepository _tours;
        private readonly ITourServiceRepository _services;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _clock;
        private readonly TourBusTransferCommandValidator _validator;

        public ReplaceTourBusTransferHandler(
            ITourRepository tours,
            ITourServiceRepository services,
            IUnitOfWork unitOfWork,
            IDateTimeProvider clock,
            TourBusTransferCommandValidator validator
        )
        {
            _tours = tours;
            _services = services;
            _unitOfWork = unitOfWork;
            _clock = clock;
            _validator = validator;
        }

        public async Task<TourBusTransferDto> Handle(
            ReplaceTourBusTransferCommand command,
            CancellationToken cancellationToken
        )
        {
            _validator.ValidateAndThrow(command);

            var tour = await _tours.GetByIdAsync(command.TourId, cancellationToken);
            if (tour == null)
                throw new NotFoundException(ErrorMessages.TourNotFound, ErrorCode.TourNotFound);

            var service = await GetService(
                command.TourId,
                command.TourServiceId,
                cancellationToken
            );
            var transfer = await _services.GetBusTransferAsync(service.Id, cancellationToken);
            var now = _clock.UtcNow;

            if (transfer == null)
            {
                transfer = new TourBusTransfer(
                    service.Id,
                    command.DepartureTimeFrom,
                    command.DepartureTimeTo,
                    command.ArrivalTimeFrom,
                    command.ArrivalTimeTo,
                    now
                );
                await _services.AddBusTransferAsync(transfer, cancellationToken);
            }
            else
            {
                _services.RemoveBusTransferDetails(transfer);
                transfer.UpdateTimeRanges(
                    command.DepartureTimeFrom,
                    command.DepartureTimeTo,
                    command.ArrivalTimeFrom,
                    command.ArrivalTimeTo,
                    now
                );
            }

            transfer.ReplaceDepartureCountries(command.DepartureCountryIds);
            transfer.ReplaceDepartureCities(command.DepartureCityIds);
            transfer.ReplaceArrivalCountries(command.ArrivalCountryIds);
            transfer.ReplaceArrivalCities(command.ArrivalCityIds);
            transfer.ReplaceOptions(command.BusTransferIds);
            transfer.ReplaceBoardingPoints(command.BoardingPointIds);
            transfer.ReplaceTransports(command.TransportIds);
            service.IncrementRevision(now);
            tour.IncrementRevision();
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return transfer.ToDto();
        }

        private async Task<TourService> GetService(
            int tourId,
            int serviceId,
            CancellationToken cancellationToken
        )
        {
            var service = await _services.GetByIdAsync(tourId, serviceId, cancellationToken);
            if (service == null)
                throw new NotFoundException(
                    ErrorMessages.TourServiceNotFound,
                    ErrorCode.TourServiceNotFound
                );
            if (service.ServiceTypeId != (int)ServiceTypeEnum.BusTransfer)
                throw new ValidationException(
                    new[]
                    {
                        "Bus transfer settings are allowed only for the BusTransfer service type.",
                    }
                );
            return service;
        }
    }

    public class GetTourBusTransferHandler
        : IQueryHandler<GetTourBusTransferQuery, TourBusTransferDto>
    {
        private readonly ITourServiceRepository _services;

        public GetTourBusTransferHandler(ITourServiceRepository services)
        {
            _services = services;
        }

        public async Task<TourBusTransferDto> Handle(
            GetTourBusTransferQuery query,
            CancellationToken cancellationToken
        )
        {
            var service = await _services.GetByIdAsync(
                query.TourId,
                query.TourServiceId,
                cancellationToken
            );
            if (service == null)
                throw new NotFoundException(
                    ErrorMessages.TourServiceNotFound,
                    ErrorCode.TourServiceNotFound
                );
            if (service.ServiceTypeId != (int)ServiceTypeEnum.BusTransfer)
                throw new ValidationException(
                    new[]
                    {
                        "Bus transfer settings are allowed only for the BusTransfer service type.",
                    }
                );

            var transfer = await _services.GetBusTransferAsync(service.Id, cancellationToken);
            return transfer == null ? TourBusTransferMapping.Empty() : transfer.ToDto();
        }
    }
}
