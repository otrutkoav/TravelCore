using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Abstractions.Services;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.HotelCategories.Commands;
using TourCore.Application.HotelCategories.DTOs;
using TourCore.Application.HotelCategories.Mappings;
using TourCore.Application.HotelCategories.Validators;
using TourCore.Domain.Hotels.Entities;

namespace TourCore.Application.HotelCategories.Handlers
{
    public class CreateHotelCategoryHandler : ICommandHandler<CreateHotelCategoryCommand, HotelCategoryDto>
    {
        private readonly IHotelCategoryRepository _hotelCategoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly CreateHotelCategoryCommandValidator _validator;

        public CreateHotelCategoryHandler(
            IHotelCategoryRepository hotelCategoryRepository,
            IUnitOfWork unitOfWork,
            IDateTimeProvider dateTimeProvider,
            CreateHotelCategoryCommandValidator validator)
        {
            _hotelCategoryRepository = hotelCategoryRepository;
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
            _validator = validator;
        }

        public async Task<HotelCategoryDto> Handle(CreateHotelCategoryCommand command, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(command);

            string normalizedGlobalCode = null;

            if (!string.IsNullOrWhiteSpace(command.GlobalCode))
            {
                normalizedGlobalCode = command.GlobalCode.Trim().ToUpperInvariant();

                if (await _hotelCategoryRepository.ExistsByGlobalCodeAsync(normalizedGlobalCode, cancellationToken))
                    throw new ConflictException("Hotel category with same global code already exists.");
            }

            var entity = new HotelCategory(
                command.Name,
                _dateTimeProvider.UtcNow,
                command.NameEn,
                command.PrintOrder,
                normalizedGlobalCode,
                command.Description);

            await _hotelCategoryRepository.AddAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return entity.ToDto();
        }
    }
}