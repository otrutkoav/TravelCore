using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Abstractions.Services;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.ServiceTypes.Commands;
using TourCore.Application.ServiceTypes.DTOs;
using TourCore.Application.ServiceTypes.Mappings;
using TourCore.Application.ServiceTypes.Validators;
using TourCore.Domain.Services.Entities;

namespace TourCore.Application.ServiceTypes.Handlers
{
    public class CreateServiceTypeHandler : ICommandHandler<CreateServiceTypeCommand, ServiceTypeDto>
    {
        private readonly IServiceTypeRepository _serviceTypeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly CreateServiceTypeCommandValidator _validator;

        public CreateServiceTypeHandler(
            IServiceTypeRepository serviceTypeRepository,
            IUnitOfWork unitOfWork,
            IDateTimeProvider dateTimeProvider,
            CreateServiceTypeCommandValidator validator)
        {
            _serviceTypeRepository = serviceTypeRepository;
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
            _validator = validator;
        }

        public async Task<ServiceTypeDto> Handle(CreateServiceTypeCommand command, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(command);

            if (!string.IsNullOrWhiteSpace(command.Code))
            {
                var normalizedCode = command.Code.Trim().ToUpperInvariant();

                if (await _serviceTypeRepository.ExistsByCodeAsync(normalizedCode, cancellationToken))
                    throw new ConflictException("Service type with same code exists.");
            }

            var entity = new ServiceType(
                command.Name,
                _dateTimeProvider.UtcNow,
                command.Code,
                command.NameEn,
                command.Category,
                command.ControlMode,
                command.IsCity,
                command.HasPrimaryParameter,
                command.HasSecondaryParameter,
                command.RoundGrossAmount,
                command.IsDuration,
                command.UseManualInput,
                command.IsQuoted,
                command.IsIndividual,
                command.SmallAmountPercent,
                command.SmallAmountThreshold,
                command.UseSmallAmountAndRule,
                command.IsRoute,
                command.IsPartnerBasedOn);

            await _serviceTypeRepository.AddAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return entity.ToDto();
        }
    }
}