using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Abstractions.Services;
using TourCore.Application.Transports.Commands;
using TourCore.Application.Transports.DTOs;
using TourCore.Application.Transports.Mappings;
using TourCore.Application.Transports.Validators;
using TourCore.Domain.Transportation.Entities;

namespace TourCore.Application.Transports.Handlers
{
    public class CreateTransportHandler : ICommandHandler<CreateTransportCommand, TransportDto>
    {
        private readonly ITransportRepository _transportRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly CreateTransportCommandValidator _validator;

        public CreateTransportHandler(
            ITransportRepository transportRepository,
            IUnitOfWork unitOfWork,
            IDateTimeProvider dateTimeProvider,
            CreateTransportCommandValidator validator)
        {
            _transportRepository = transportRepository;
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
            _validator = validator;
        }

        public async Task<TransportDto> Handle(CreateTransportCommand command, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(command);

            var entity = new Transport(
                command.Name,
                _dateTimeProvider.UtcNow,
                command.NameEn,
                command.SeatsCount,
                command.ShowOrder);

            await _transportRepository.AddAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return entity.ToDto();
        }
    }
}