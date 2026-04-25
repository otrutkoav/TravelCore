using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Abstractions.Services;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Rates.Commands;
using TourCore.Contracts.Finance.Rates;
using TourCore.Application.Rates.Mappings;
using TourCore.Application.Rates.Validators;
using TourCore.Domain.Finance.Entities;

namespace TourCore.Application.Rates.Handlers
{
    public class CreateRateHandler : ICommandHandler<CreateRateCommand, RateDto>
    {
        private readonly IRateRepository _rateRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly CreateRateCommandValidator _validator;

        public CreateRateHandler(
            IRateRepository rateRepository,
            IUnitOfWork unitOfWork,
            IDateTimeProvider dateTimeProvider,
            CreateRateCommandValidator validator)
        {
            _rateRepository = rateRepository;
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
            _validator = validator;
        }

        public async Task<RateDto> Handle(CreateRateCommand command, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(command);

            var normalizedCode = command.Code.Trim();

            if (await _rateRepository.ExistsByCodeAsync(normalizedCode, cancellationToken))
                throw new ConflictException("Rate with the same code already exists.");

            if (!string.IsNullOrWhiteSpace(command.IsoCode))
            {
                var normalizedIsoCode = command.IsoCode.Trim().ToUpperInvariant();

                if (await _rateRepository.ExistsByIsoCodeAsync(normalizedIsoCode, cancellationToken))
                    throw new ConflictException("Rate with the same ISO code already exists.");
            }

            var entity = new Rate(
                command.Code,
                command.Name,
                _dateTimeProvider.UtcNow,
                command.IsoCode,
                command.IsMain,
                command.IsNational,
                command.ShowInSearch,
                command.Symbol);

            await _rateRepository.AddAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return entity.ToDto();
        }
    }
}