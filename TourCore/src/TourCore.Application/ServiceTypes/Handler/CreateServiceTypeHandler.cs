using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Abstractions.Services;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.ServiceTypes.Commands;
using TourCore.Application.ServiceTypes.DTOs;
using TourCore.Application.ServiceTypes.Validators;
using TourCore.Application.ServiceTypes.Mappings;
using TourCore.Domain.Services.Entities;

public class CreateServiceTypeHandler
    : ICommandHandler<CreateServiceTypeCommand, ServiceTypeDto>
{
    private readonly IServiceTypeRepository _repo;
    private readonly IUnitOfWork _uow;
    private readonly IDateTimeProvider _date;
    private readonly CreateServiceTypeCommandValidator _validator;

    public CreateServiceTypeHandler(
        IServiceTypeRepository repo,
        IUnitOfWork uow,
        IDateTimeProvider date,
        CreateServiceTypeCommandValidator validator)
    {
        _repo = repo;
        _uow = uow;
        _date = date;
        _validator = validator;
    }

    public async Task<ServiceTypeDto> Handle(CreateServiceTypeCommand cmd, CancellationToken ct)
    {
        _validator.ValidateAndThrow(cmd);

        if (!string.IsNullOrWhiteSpace(cmd.Code))
        {
            var code = cmd.Code.Trim().ToUpperInvariant();

            if (await _repo.ExistsByCodeAsync(code, ct))
                throw new ConflictException("Service type with same code exists.");
        }

        var entity = new ServiceType(
            cmd.Name,
            _date.UtcNow,
            cmd.Code,
            cmd.NameEn,
            cmd.Category,
            cmd.ControlMode,
            cmd.IsCity,
            cmd.HasPrimaryParameter,
            cmd.HasSecondaryParameter,
            cmd.RoundGrossAmount,
            cmd.IsDuration,
            cmd.UseManualInput,
            cmd.IsQuoted,
            cmd.IsIndividual,
            cmd.SmallAmountPercent,
            cmd.SmallAmountThreshold,
            cmd.UseSmallAmountAndRule,
            cmd.IsRoute,
            cmd.IsPartnerBasedOn);

        await _repo.AddAsync(entity, ct);
        await _uow.SaveChangesAsync(ct);

        return entity.ToDto();
    }
}