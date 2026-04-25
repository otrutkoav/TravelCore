using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Abstractions.Services;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.RealCourses.Commands;
using TourCore.Contracts.Finance.RealCourses;
using TourCore.Application.RealCourses.Mappings;
using TourCore.Application.RealCourses.Validators;
using TourCore.Domain.Finance.Entities;

namespace TourCore.Application.RealCourses.Handlers
{
    public class CreateRealCourseHandler : ICommandHandler<CreateRealCourseCommand, RealCourseDto>
    {
        private readonly IRealCourseRepository _realCourseRepository;
        private readonly IRateRepository _rateRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly CreateRealCourseCommandValidator _validator;

        public CreateRealCourseHandler(
            IRealCourseRepository realCourseRepository,
            IRateRepository rateRepository,
            IUnitOfWork unitOfWork,
            IDateTimeProvider dateTimeProvider,
            CreateRealCourseCommandValidator validator)
        {
            _realCourseRepository = realCourseRepository;
            _rateRepository = rateRepository;
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
            _validator = validator;
        }

        public async Task<RealCourseDto> Handle(CreateRealCourseCommand command, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(command);

            var fromRateCode = command.FromRateCode.Trim();
            var toRateCode = command.ToRateCode.Trim();

            if (fromRateCode == toRateCode)
                throw new ValidationException(new[] { "FromRateCode and ToRateCode must be different." });

            if (!await _rateRepository.ExistsByCodeValueAsync(fromRateCode, cancellationToken))
                throw new NotFoundException("Source rate was not found.");

            if (!await _rateRepository.ExistsByCodeValueAsync(toRateCode, cancellationToken))
                throw new NotFoundException("Target rate was not found.");

            if (await _realCourseRepository.ExistsAsync(
                fromRateCode,
                toRateCode,
                command.DateBeg,
                command.DateEnd,
                cancellationToken))
            {
                throw new ConflictException("Real course with the same rate pair and period already exists.");
            }

            var entity = new RealCourse(
                command.FromRateCode,
                command.ToRateCode,
                _dateTimeProvider.UtcNow,
                command.Course,
                command.CentralBankCourse,
                command.DateBeg,
                command.DateEnd);

            await _realCourseRepository.AddAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return entity.ToDto();
        }
    }
}