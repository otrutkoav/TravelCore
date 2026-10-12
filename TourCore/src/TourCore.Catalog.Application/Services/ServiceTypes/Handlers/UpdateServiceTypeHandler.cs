using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Abstractions.Persistence.Services;
using TourCore.Application.Abstractions.Services;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Common.Errors;
using TourCore.Application.Services.ServiceTypes.Commands;
using TourCore.Application.Services.ServiceTypes.Mapping;
using TourCore.Application.Services.ServiceTypes.Validators;
using TourCore.Contracts.Services.ServiceTypes;

namespace TourCore.Application.Services.ServiceTypes.Handlers
{
    public class UpdateServiceTypeHandler : ICommandHandler<UpdateServiceTypeCommand, ServiceTypeDto>
    {
        private readonly IServiceTypeRepository _serviceTypeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly UpdateServiceTypeCommandValidator _validator;

        public UpdateServiceTypeHandler(
            IServiceTypeRepository serviceTypeRepository,
            IUnitOfWork unitOfWork,
            IDateTimeProvider dateTimeProvider,
            UpdateServiceTypeCommandValidator validator)
        {
            _serviceTypeRepository = serviceTypeRepository;
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
            _validator = validator;
        }

        public async Task<ServiceTypeDto> Handle(UpdateServiceTypeCommand command, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(command);

            var entity = await _serviceTypeRepository.GetByIdAsync(command.Id, cancellationToken);
            if (entity == null)
                throw new NotFoundException(ErrorMessages.ServiceTypeNotFound, ErrorCode.ServiceTypeNotFound);

            if (!string.IsNullOrWhiteSpace(command.Code))
            {
                var normalizedCode = command.Code.Trim().ToUpperInvariant();

                if (await _serviceTypeRepository.ExistsByCodeAsync(normalizedCode, command.Id, cancellationToken))
                    throw new ConflictException(ErrorMessages.ServiceTypeCodeExists, ErrorCode.ServiceTypeCodeExists);
            }

            entity.Update(
                command.Name,
                _dateTimeProvider.UtcNow,
                command.Code,
                command.NameEn,
                command.IsDuration,
                command.IsRoute);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return entity.ToDto();
        }
    }
}
