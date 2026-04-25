using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Abstractions.Services;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.HotelCategories.Commands;
using TourCore.Contracts.Hotels.HotelCategories;
using TourCore.Application.HotelCategories.Mappings;
using TourCore.Application.HotelCategories.Validators;

namespace TourCore.Application.HotelCategories.Handlers
{
    public class UpdateHotelCategoryHandler : ICommandHandler<UpdateHotelCategoryCommand, HotelCategoryDto>
    {
        private readonly IHotelCategoryRepository _hotelCategoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly UpdateHotelCategoryCommandValidator _validator;

        public UpdateHotelCategoryHandler(
            IHotelCategoryRepository hotelCategoryRepository,
            IUnitOfWork unitOfWork,
            IDateTimeProvider dateTimeProvider,
            UpdateHotelCategoryCommandValidator validator)
        {
            _hotelCategoryRepository = hotelCategoryRepository;
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
            _validator = validator;
        }

        public async Task<HotelCategoryDto> Handle(UpdateHotelCategoryCommand command, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(command);

            var entity = await _hotelCategoryRepository.GetByIdAsync(command.Id, cancellationToken);
            if (entity == null)
                throw new NotFoundException("Hotel category was not found.");

            string normalizedGlobalCode = null;

            if (!string.IsNullOrWhiteSpace(command.GlobalCode))
            {
                normalizedGlobalCode = command.GlobalCode.Trim().ToUpperInvariant();

                if (await _hotelCategoryRepository.ExistsByGlobalCodeAsync(normalizedGlobalCode, command.Id, cancellationToken))
                    throw new ConflictException("Hotel category with same global code already exists.");
            }

            entity.Update(
                command.Name,
                _dateTimeProvider.UtcNow,
                command.NameEn,
                command.PrintOrder,
                normalizedGlobalCode,
                command.Description);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return entity.ToDto();
        }
    }
}