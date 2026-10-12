using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Abstractions.Services;
using TourCore.Application.Common.Exceptions;
using TourCore.Contracts.Finance.RealCourses;
using TourCore.Application.Abstractions.Persistence.Finance;
using TourCore.Application.Finance.RealCourses.Commands;
using TourCore.Application.Finance.RealCourses.Mappings;
using TourCore.Application.Finance.RealCourses.Validators;
using System.Collections.Generic;
using TourCore.Application.Common.Errors;

namespace TourCore.Application.Finance.RealCourses.Handlers
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
                throw new NotFoundException(ErrorMessages.RealCourseNotFound, ErrorCode.RealCourseNotFound);

            var fromRateCode = command.FromRateCode.Trim().ToUpperInvariant();
            var toRateCode = command.ToRateCode.Trim().ToUpperInvariant();

            if (fromRateCode == toRateCode)
                throw new ValidationException(new Dictionary<string, string[]>
        {
            { "ToRateCode", new[] { ErrorCode.SameRateCodes } }
        });

            if (!await _rateRepository.ExistsByCodeValueAsync(fromRateCode, cancellationToken))
                throw new NotFoundException(ErrorMessages.FromRateNotFound, ErrorCode.FromRateNotFound);

            if (!await _rateRepository.ExistsByCodeValueAsync(toRateCode, cancellationToken))
                throw new NotFoundException(ErrorMessages.ToRateNotFound, ErrorCode.ToRateNotFound);

            if (await _realCourseRepository.ExistsAsync(
                fromRateCode,
                toRateCode,
                command.DateBeg,
                command.DateEnd,
                command.Id,
                cancellationToken))
            {
                throw new ConflictException(ErrorMessages.RealCourseExists, ErrorCode.RealCourseExists);
            }

            entity.Update(
                fromRateCode,
                toRateCode,
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