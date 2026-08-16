using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Abstractions.Persistence.Services;
using TourCore.Application.Abstractions.Persistence.Tours;
using TourCore.Application.Abstractions.Services;
using TourCore.Application.Common.Errors;
using TourCore.Application.Common.Exceptions;
using TourCore.Contracts.Tours;
using TourCore.Domain.Tours;

namespace TourCore.Application.Tours
{
    public abstract class TourServiceHandlerBase
    {
        protected readonly ITourRepository tours;
        protected readonly ITourServiceRepository services;
        protected readonly IServiceTypeRepository types;
        protected readonly IUnitOfWork unit;
        protected readonly IDateTimeProvider clock;
        protected readonly TourServiceCommandValidator validator;

        protected TourServiceHandlerBase(
            ITourRepository a,
            ITourServiceRepository b,
            IServiceTypeRepository c,
            IUnitOfWork d,
            IDateTimeProvider e,
            TourServiceCommandValidator f
        )
        {
            tours = a;
            services = b;
            types = c;
            unit = d;
            clock = e;
            validator = f;
        }

        protected async Task<Tour> Tour(int id, CancellationToken ct)
        {
            var x = await tours.GetByIdAsync(id, ct);
            if (x == null)
                throw new NotFoundException(ErrorMessages.TourNotFound, ErrorCode.TourNotFound);
            return x;
        }

        protected async Task EnsureType(int id, CancellationToken ct)
        {
            if (await types.GetByIdAsync(id, ct) == null)
                throw new NotFoundException(
                    ErrorMessages.ServiceTypeNotFound,
                    ErrorCode.ServiceTypeNotFound
                );
        }

        protected async Task EnsureParent(
            int tourId,
            int? parentId,
            int? selfId,
            CancellationToken ct
        )
        {
            if (!parentId.HasValue)
                return;
            if (parentId == selfId)
                throw new ValidationException(new[] { "A service cannot be its own parent." });
            var p = await services.GetByIdAsync(tourId, parentId.Value, ct);
            if (p == null)
                throw new NotFoundException(
                    ErrorMessages.TourServiceParentNotFound,
                    ErrorCode.TourServiceParentNotFound
                );
            if (
                selfId.HasValue
                && await services.IsDescendantAsync(selfId.Value, parentId.Value, ct)
            )
                throw new ValidationException(new[] { "ParentServiceId creates a cycle." });
        }

        protected async Task<TourService> Service(int tourId, int id, CancellationToken ct)
        {
            var x = await services.GetByIdAsync(tourId, id, ct);
            if (x == null)
                throw new NotFoundException(
                    ErrorMessages.TourServiceNotFound,
                    ErrorCode.TourServiceNotFound
                );
            return x;
        }
    }

    public class CreateTourServiceHandler
        : TourServiceHandlerBase,
            ICommandHandler<CreateTourServiceCommand, TourServiceDto>
    {
        public CreateTourServiceHandler(
            ITourRepository a,
            ITourServiceRepository b,
            IServiceTypeRepository c,
            IUnitOfWork d,
            IDateTimeProvider e,
            TourServiceCommandValidator f
        )
            : base(a, b, c, d, e, f) { }

        public async Task<TourServiceDto> Handle(CreateTourServiceCommand c, CancellationToken ct)
        {
            validator.Validate(c);
            var tour = await Tour(c.TourId, ct);
            await EnsureType(c.ServiceTypeId, ct);
            await EnsureParent(c.TourId, c.ParentServiceId, null, ct);
            var x = new TourService(
                c.TourId,
                c.ServiceTypeId,
                c.Name,
                await services.GetNextOrderIndexAsync(c.TourId, ct),
                c.ParentServiceId,
                c.IsHidden,
                c.IsRemovable,
                c.IsOptional,
                clock.UtcNow
            );
            await services.AddAsync(x, ct);
            tour.IncrementRevision();
            await unit.SaveChangesAsync(ct);
            return x.ToDto();
        }
    }

    public class UpdateTourServiceHandler
        : TourServiceHandlerBase,
            ICommandHandler<UpdateTourServiceCommand, TourServiceDto>
    {
        public UpdateTourServiceHandler(
            ITourRepository a,
            ITourServiceRepository b,
            IServiceTypeRepository c,
            IUnitOfWork d,
            IDateTimeProvider e,
            TourServiceCommandValidator f
        )
            : base(a, b, c, d, e, f) { }

        public async Task<TourServiceDto> Handle(UpdateTourServiceCommand c, CancellationToken ct)
        {
            validator.Validate(c);
            var tour = await Tour(c.TourId, ct);
            var x = await Service(c.TourId, c.Id, ct);
            await EnsureParent(c.TourId, c.ParentServiceId, c.Id, ct);
            x.Update(
                c.Name,
                c.ParentServiceId,
                c.IsHidden,
                c.IsRemovable,
                c.IsOptional,
                clock.UtcNow
            );
            tour.IncrementRevision();
            await unit.SaveChangesAsync(ct);
            return x.ToDto();
        }
    }

    public class DeleteTourServiceHandler
        : TourServiceHandlerBase,
            ICommandHandler<DeleteTourServiceCommand, bool>
    {
        public DeleteTourServiceHandler(
            ITourRepository a,
            ITourServiceRepository b,
            IServiceTypeRepository c,
            IUnitOfWork d,
            IDateTimeProvider e,
            TourServiceCommandValidator f
        )
            : base(a, b, c, d, e, f) { }

        public async Task<bool> Handle(DeleteTourServiceCommand c, CancellationToken ct)
        {
            var tour = await Tour(c.TourId, ct);
            var x = await Service(c.TourId, c.Id, ct);
            if (await services.HasChildrenAsync(c.Id, ct))
                throw new ConflictException(
                    ErrorMessages.TourServiceHasChildren,
                    ErrorCode.TourServiceHasChildren
                );
            services.Remove(x);
            var remaining = (await services.ListByTourIdAsync(c.TourId, ct))
                .Where(s => s.Id != c.Id)
                .OrderBy(s => s.OrderIndex)
                .ToArray();
            for (var i = 0; i < remaining.Length; i++)
                if (remaining[i].OrderIndex != i + 1)
                    remaining[i].MoveTo(i + 1, clock.UtcNow);
            tour.IncrementRevision();
            await unit.SaveChangesAsync(ct);
            return true;
        }
    }

    public class ReorderTourServicesHandler
        : TourServiceHandlerBase,
            ICommandHandler<ReorderTourServicesCommand, IReadOnlyCollection<TourServiceDto>>
    {
        public ReorderTourServicesHandler(
            ITourRepository a,
            ITourServiceRepository b,
            IServiceTypeRepository c,
            IUnitOfWork d,
            IDateTimeProvider e,
            TourServiceCommandValidator f
        )
            : base(a, b, c, d, e, f) { }

        public async Task<IReadOnlyCollection<TourServiceDto>> Handle(
            ReorderTourServicesCommand c,
            CancellationToken ct
        )
        {
            validator.Validate(c);
            var tour = await Tour(c.TourId, ct);
            var all = (await services.ListByTourIdAsync(c.TourId, ct)).ToArray();
            if (
                all.Length != c.ServiceIds.Count
                || !all.Select(x => x.Id)
                    .OrderBy(x => x)
                    .SequenceEqual(c.ServiceIds.OrderBy(x => x))
            )
                throw new ValidationException(
                    new[] { "ServiceIds must contain every service of the tour exactly once." }
                );
            var byId = all.ToDictionary(x => x.Id);
            var position = 1;
            foreach (var id in c.ServiceIds)
            {
                var x = byId[id];
                if (x.OrderIndex != position)
                    x.MoveTo(position, clock.UtcNow);
                position++;
            }
            tour.IncrementRevision();
            await unit.SaveChangesAsync(ct);
            return c.ServiceIds.Select(id => byId[id].ToDto()).ToArray();
        }
    }

    public class GetTourServicesHandler
        : IQueryHandler<GetTourServicesQuery, IReadOnlyCollection<TourServiceDto>>
    {
        private readonly ITourRepository tours;
        private readonly ITourServiceRepository services;

        public GetTourServicesHandler(ITourRepository a, ITourServiceRepository b)
        {
            tours = a;
            services = b;
        }

        public async Task<IReadOnlyCollection<TourServiceDto>> Handle(
            GetTourServicesQuery q,
            CancellationToken ct
        )
        {
            if (await tours.GetByIdAsync(q.TourId, ct) == null)
                throw new NotFoundException(ErrorMessages.TourNotFound, ErrorCode.TourNotFound);
            return (await services.ListByTourIdAsync(q.TourId, ct))
                .OrderBy(x => x.OrderIndex)
                .Select(x => x.ToDto())
                .ToArray();
        }
    }

    public class GetTourServiceByIdHandler : IQueryHandler<GetTourServiceByIdQuery, TourServiceDto>
    {
        private readonly ITourServiceRepository services;

        public GetTourServiceByIdHandler(ITourServiceRepository x)
        {
            services = x;
        }

        public async Task<TourServiceDto> Handle(GetTourServiceByIdQuery q, CancellationToken ct)
        {
            var x = await services.GetByIdAsync(q.TourId, q.Id, ct);
            if (x == null)
                throw new NotFoundException(
                    ErrorMessages.TourServiceNotFound,
                    ErrorCode.TourServiceNotFound
                );
            return x.ToDto();
        }
    }
}
