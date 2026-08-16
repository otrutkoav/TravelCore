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
    public class ReplaceTourRailwayTransferHandler
        : ICommandHandler<ReplaceTourRailwayTransferCommand, TourRailwayTransferDto>
    {
        private readonly ITourRepository _tours;
        private readonly ITourServiceRepository _services;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _clock;
        private readonly TourRailwayTransferCommandValidator _validator;

        public ReplaceTourRailwayTransferHandler(
            ITourRepository tours,
            ITourServiceRepository services,
            IUnitOfWork unitOfWork,
            IDateTimeProvider clock,
            TourRailwayTransferCommandValidator validator
        )
        {
            _tours = tours;
            _services = services;
            _unitOfWork = unitOfWork;
            _clock = clock;
            _validator = validator;
        }

        public async Task<TourRailwayTransferDto> Handle(
            ReplaceTourRailwayTransferCommand command,
            CancellationToken ct
        )
        {
            _validator.ValidateAndThrow(command);
            var tour = await _tours.GetByIdAsync(command.TourId, ct);
            if (tour == null)
                throw new NotFoundException(ErrorMessages.TourNotFound, ErrorCode.TourNotFound);
            var service = await GetService(command.TourId, command.TourServiceId, ct);
            var transfer = await _services.GetRailwayTransferAsync(service.Id, ct);
            var now = _clock.UtcNow;
            if (transfer == null)
            {
                transfer = new TourRailwayTransfer(
                    service.Id,
                    command.DepartureTimeFrom,
                    command.DepartureTimeTo,
                    command.ArrivalTimeFrom,
                    command.ArrivalTimeTo,
                    now
                );
                await _services.AddRailwayTransferAsync(transfer, ct);
            }
            else
            {
                _services.RemoveRailwayTransferDetails(transfer);
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
            transfer.ReplaceOptions(command.RailwayTransferIds);
            transfer.ReplaceTransports(command.TransportIds);
            service.IncrementRevision(now);
            tour.IncrementRevision();
            await _unitOfWork.SaveChangesAsync(ct);
            return transfer.ToDto();
        }

        private async Task<TourService> GetService(int tourId, int serviceId, CancellationToken ct)
        {
            var service = await _services.GetByIdAsync(tourId, serviceId, ct);
            if (service == null)
                throw new NotFoundException(
                    ErrorMessages.TourServiceNotFound,
                    ErrorCode.TourServiceNotFound
                );
            if (service.ServiceTypeId != (int)ServiceTypeEnum.RailwayTransfer)
                throw new ValidationException(
                    new[]
                    {
                        "Railway transfer settings are allowed only for the RailwayTransfer service type.",
                    }
                );
            return service;
        }
    }

    public class GetTourRailwayTransferHandler
        : IQueryHandler<GetTourRailwayTransferQuery, TourRailwayTransferDto>
    {
        private readonly ITourServiceRepository _services;

        public GetTourRailwayTransferHandler(ITourServiceRepository services)
        {
            _services = services;
        }

        public async Task<TourRailwayTransferDto> Handle(
            GetTourRailwayTransferQuery query,
            CancellationToken ct
        )
        {
            var service = await _services.GetByIdAsync(query.TourId, query.TourServiceId, ct);
            if (service == null)
                throw new NotFoundException(
                    ErrorMessages.TourServiceNotFound,
                    ErrorCode.TourServiceNotFound
                );
            if (service.ServiceTypeId != (int)ServiceTypeEnum.RailwayTransfer)
                throw new ValidationException(
                    new[]
                    {
                        "Railway transfer settings are allowed only for the RailwayTransfer service type.",
                    }
                );
            var transfer = await _services.GetRailwayTransferAsync(service.Id, ct);
            return transfer == null ? TourRailwayTransferMapping.Empty() : transfer.ToDto();
        }
    }
}
