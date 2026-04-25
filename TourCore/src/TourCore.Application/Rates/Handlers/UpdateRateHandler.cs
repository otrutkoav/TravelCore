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

namespace TourCore.Application.Rates.Handlers
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
                throw new NotFoundException("Rate was not found.");

            var normalizedCode = command.Code.Trim().ToUpperInvariant();

            if (await _rateRepository.ExistsByCodeAsync(normalizedCode, command.Id, cancellationToken))
                throw new ConflictException("Rate with same code already exists.");

            entity.Update(
                normalizedCode,
                command.Name,
                _dateTimeProvider.UtcNow,
                command.IsoCode,
                command.IsMain,
                command.IsNational,
                command.ShowInSearch,
                command.Symbol);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return entity.ToDto();
        }
    }
}