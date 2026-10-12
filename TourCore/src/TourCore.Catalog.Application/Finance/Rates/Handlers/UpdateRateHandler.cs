using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Abstractions.Services;
using TourCore.Application.Common.Exceptions;
using TourCore.Contracts.Finance.Rates;
using TourCore.Application.Abstractions.Persistence.Finance;
using TourCore.Application.Finance.Rates.Commands;
using TourCore.Application.Finance.Rates.Mappings;
using TourCore.Application.Finance.Rates.Validators;
using TourCore.Application.Common.Errors;

namespace TourCore.Application.Finance.Rates.Handlers
{
    public class UpdateRateHandler : ICommandHandler<UpdateRateCommand, RateDto>
    {
        private readonly IRateRepository _rateRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly UpdateRateCommandValidator _validator;

        public UpdateRateHandler(
            IRateRepository rateRepository,
            IUnitOfWork unitOfWork,
            IDateTimeProvider dateTimeProvider,
            UpdateRateCommandValidator validator)
        {
            _rateRepository = rateRepository;
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
            _validator = validator;
        }

        public async Task<RateDto> Handle(UpdateRateCommand command, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(command);

            var entity = await _rateRepository.GetByIdAsync(command.Id, cancellationToken);
            if (entity == null)
                throw new NotFoundException(ErrorMessages.RateNotFound, ErrorCode.RateNotFound);

            var normalizedCode = command.Code.Trim().ToUpperInvariant();

            if (await _rateRepository.ExistsByCodeAsync(normalizedCode, command.Id, cancellationToken))
                throw new ConflictException(ErrorMessages.RateCodeExists, ErrorCode.RateCodeExists);

            string normalizedIsoCode = null;

            if (!string.IsNullOrWhiteSpace(command.IsoCode))
            {
                normalizedIsoCode = command.IsoCode.Trim().ToUpperInvariant();

                if (await _rateRepository.ExistsByIsoCodeAsync(normalizedIsoCode, command.Id, cancellationToken))
                    throw new ConflictException(ErrorMessages.RateIsoCodeExists, ErrorCode.RateIsoCodeExists);
            }

            entity.Update(
                normalizedCode,
                command.Name,
                _dateTimeProvider.UtcNow,
                normalizedIsoCode,
                command.IsMain,
                command.IsNational,
                command.ShowInSearch,
                command.Symbol);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return entity.ToDto();
        }
    }
}