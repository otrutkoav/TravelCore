using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Abstractions.Services;
using TourCore.Application.CharterSeasons.Commands;
using TourCore.Contracts.Avia.CharterSeasons;
using TourCore.Application.CharterSeasons.Mappings;
using TourCore.Application.CharterSeasons.Validators;
using TourCore.Application.Common.Exceptions;
using TourCore.Domain.Common.ValueObjects;

namespace TourCore.Application.CharterSeasons.Handlers
{
    public class UpdateCharterSeasonHandler : ICommandHandler<UpdateCharterSeasonCommand, CharterSeasonDto>
    {
        private readonly ICharterSeasonRepository _charterSeasonRepository;
        private readonly ICharterRepository _charterRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly UpdateCharterSeasonCommandValidator _validator;

        public UpdateCharterSeasonHandler(
            ICharterSeasonRepository charterSeasonRepository,
            ICharterRepository charterRepository,
            IUnitOfWork unitOfWork,
            IDateTimeProvider dateTimeProvider,
            UpdateCharterSeasonCommandValidator validator)
        {
            _charterSeasonRepository = charterSeasonRepository;
            _charterRepository = charterRepository;
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
            _validator = validator;
        }

        public async Task<CharterSeasonDto> Handle(UpdateCharterSeasonCommand command, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(command);

            var entity = await _charterSeasonRepository.GetByIdAsync(command.Id, cancellationToken);
            if (entity == null)
                throw new NotFoundException("Charter season was not found.");

            var charter = await _charterRepository.GetByIdAsync(command.CharterId, cancellationToken);
            if (charter == null)
                throw new NotFoundException("Charter was not found.");

            entity.Update(
                command.CharterId,
                _dateTimeProvider.UtcNow,
                command.DateFrom,
                command.DateTo,
                DaysOfWeek.FromLegacy(command.DaysOfWeek),
                command.TimeFrom,
                command.TimeTo,
                command.IsNextDayArrival,
                command.Remark);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return entity.ToDto();
        }
    }
}