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
using TourCore.Domain.Avia.Entities;
using TourCore.Domain.Common.ValueObjects;

namespace TourCore.Application.CharterSeasons.Handlers
{
    public class CreateCharterSeasonHandler : ICommandHandler<CreateCharterSeasonCommand, CharterSeasonDto>
    {
        private readonly ICharterSeasonRepository _charterSeasonRepository;
        private readonly ICharterRepository _charterRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly CreateCharterSeasonCommandValidator _validator;

        public CreateCharterSeasonHandler(
            ICharterSeasonRepository charterSeasonRepository,
            ICharterRepository charterRepository,
            IUnitOfWork unitOfWork,
            IDateTimeProvider dateTimeProvider,
            CreateCharterSeasonCommandValidator validator)
        {
            _charterSeasonRepository = charterSeasonRepository;
            _charterRepository = charterRepository;
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
            _validator = validator;
        }

        public async Task<CharterSeasonDto> Handle(CreateCharterSeasonCommand command, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(command);

            var charter = await _charterRepository.GetByIdAsync(command.CharterId, cancellationToken);
            if (charter == null)
                throw new NotFoundException("Charter was not found.");

            var entity = new CharterSeason(
                command.CharterId,
                _dateTimeProvider.UtcNow,
                command.DateFrom,
                command.DateTo,
                DaysOfWeek.FromLegacy(command.DaysOfWeek),
                command.TimeFrom,
                command.TimeTo,
                command.IsNextDayArrival,
                command.Remark);

            await _charterSeasonRepository.AddAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return entity.ToDto();
        }
    }
}