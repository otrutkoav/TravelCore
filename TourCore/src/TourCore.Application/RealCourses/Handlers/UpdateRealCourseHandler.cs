using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Abstractions.Services;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.RealCourses.Commands;
using TourCore.Application.RealCourses.DTOs;
using TourCore.Application.RealCourses.Mappings;
using TourCore.Application.RealCourses.Validators;

namespace TourCore.Application.RealCourses.Handlers
{
    public class UpdateRealCourseHandler : ICommandHandler<UpdateRealCourseCommand, RealCourseDto>
    {
        private readonly IRealCourseRepository _realCourseRepository;
        private readonly IRateRepository _rateRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly UpdateRealCourseCommandValidator _validator;

        public UpdateRealCourseHandler(
            IRealCourseRepository realCourseRepository,
            IRateRepository rateRepository,
            IUnitOfWork unitOfWork,
            IDateTimeProvider dateTimeProvider,
            UpdateRealCourseCommandValidator validator)
        {
            _realCourseRepository = realCourseRepository;
            _rateRepository = rateRepository;
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
            _validator = validator;
        }

        public async Task<RealCourseDto> Handle(UpdateRealCourseCommand command, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(command);

            var entity = await _realCourseRepository.GetByIdAsync(command.Id, cancellationToken);
            if (entity == null)
                throw new NotFoundException("Real course was not found.");

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
                command.Id,
                cancellationToken))
            {
                throw new ConflictException("Real course with the same rate pair and period already exists.");
            }

            entity.Update(
                command.FromRateCode,
                command.ToRateCode,
                _dateTimeProvider.UtcNow,
                command.Course,
                command.CentralBankCourse,
                command.DateBeg,
                command.DateEnd);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return entity.ToDto();
        }
    }
}