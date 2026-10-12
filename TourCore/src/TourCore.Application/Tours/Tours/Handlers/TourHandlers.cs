using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Abstractions.Persistence.Finance;
using TourCore.Application.Abstractions.Persistence.Tours;
using TourCore.Application.Abstractions.Services;
using TourCore.Application.Common.Errors;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Common.Queries;
using TourCore.Contracts.Common;
using TourCore.Contracts.Tours;
using TourCore.Domain.Tours;

namespace TourCore.Application.Tours
{
    public abstract class TourHandlerBase
    {
        protected readonly ITourRepository tours;
        protected readonly ITourTypeRepository types;
        protected readonly IRateRepository rates;
        protected readonly IUnitOfWork unit;
        protected readonly IDateTimeProvider clock;
        protected readonly TourCommandValidator validator;

        protected TourHandlerBase(
            ITourRepository a,
            ITourTypeRepository b,
            IRateRepository c,
            IUnitOfWork d,
            IDateTimeProvider e,
            TourCommandValidator f
        )
        {
            tours = a;
            types = b;
            rates = c;
            unit = d;
            clock = e;
            validator = f;
        }

        protected async Task<IReadOnlyCollection<TourType>> ResolveTypes(
            ICollection<int> ids,
            CancellationToken ct
        )
        {
            var clean = (ids ?? new List<int>()).Distinct().ToArray();
            var found = await types.GetByIdsAsync(clean, ct);
            if (found.Count != clean.Length)
                throw new NotFoundException(
                    ErrorMessages.TourTypeNotFound,
                    ErrorCode.TourTypeNotFound
                );
            return found;
        }

        protected async Task EnsureCurrency(int id, CancellationToken ct)
        {
            if (await rates.GetByIdAsync(id, ct) == null)
                throw new NotFoundException(ErrorMessages.RateNotFound, ErrorCode.RateNotFound);
        }
    }

    public class CreateTourHandler : TourHandlerBase, ICommandHandler<CreateTourCommand, TourDto>
    {
        public CreateTourHandler(
            ITourRepository a,
            ITourTypeRepository b,
            IRateRepository c,
            IUnitOfWork d,
            IDateTimeProvider e,
            TourCommandValidator f
        )
            : base(a, b, c, d, e, f) { }

        public async Task<TourDto> Handle(CreateTourCommand c, CancellationToken ct)
        {
            validator.ValidateAndThrow(c, false);
            await EnsureCurrency(c.CurrencyId, ct);
            var selected = await ResolveTypes(c.TourTypeIds, ct);
            var x = new Tour(
                c.Name,
                c.CurrencyId,
                (TourDateMode)c.DateMode,
                c.AvailableDateFrom,
                c.AvailableDateTo,
                c.AvailableDates,
                c.DescriptionUrl,
                c.DescriptionHtml,
                c.BookingConditionsHtml,
                clock.UtcNow
            );
            x.ReplaceTourTypes(selected);
            await tours.AddAsync(x, ct);
            await unit.SaveChangesAsync(ct);
            return x.ToDto();
        }
    }

    public class UpdateTourHandler : TourHandlerBase, ICommandHandler<UpdateTourCommand, TourDto>
    {
        public UpdateTourHandler(
            ITourRepository a,
            ITourTypeRepository b,
            IRateRepository c,
            IUnitOfWork d,
            IDateTimeProvider e,
            TourCommandValidator f
        )
            : base(a, b, c, d, e, f) { }

        public async Task<TourDto> Handle(UpdateTourCommand c, CancellationToken ct)
        {
            validator.ValidateAndThrow(c, true);
            var x = await tours.GetByIdAsync(c.Id, ct);
            if (x == null)
                throw new NotFoundException(ErrorMessages.TourNotFound, ErrorCode.TourNotFound);
            await EnsureCurrency(c.CurrencyId, ct);
            var selected = await ResolveTypes(c.TourTypeIds, ct);
            var changed = CalculationChanged(x, c, selected);
            tours.RemoveAvailableDates(x.AvailableDates.ToArray());
            x.Update(
                c.Name,
                (TourStatus)c.Status,
                c.CurrencyId,
                (TourDateMode)c.DateMode,
                c.AvailableDateFrom,
                c.AvailableDateTo,
                c.AvailableDates,
                c.DescriptionUrl,
                c.DescriptionHtml,
                c.BookingConditionsHtml,
                clock.UtcNow
            );
            x.ReplaceTourTypes(selected);
            if (changed)
                x.IncrementRevision();
            await unit.SaveChangesAsync(ct);
            return x.ToDto();
        }

        private static bool CalculationChanged(
            Tour x,
            UpdateTourCommand c,
            IReadOnlyCollection<TourType> selected
        )
        {
            if (x.CurrencyId != c.CurrencyId || x.DateMode != (TourDateMode)c.DateMode)
                return true;
            if (
                (TourDateMode)c.DateMode == TourDateMode.ContinuousPeriod
                && (
                    x.AvailableDateFrom != c.AvailableDateFrom.Value.Date
                    || x.AvailableDateTo != c.AvailableDateTo.Value.Date
                )
            )
                return true;
            if (
                (TourDateMode)c.DateMode == TourDateMode.SpecificDates
                && !x
                    .AvailableDates.Select(d => d.Date)
                    .OrderBy(d => d)
                    .SequenceEqual(
                        (c.AvailableDates ?? new List<DateTime>())
                            .Select(d => d.Date)
                            .Distinct()
                            .OrderBy(d => d)
                    )
            )
                return true;
            return !x
                .TourTypes.Select(t => t.Id)
                .OrderBy(id => id)
                .SequenceEqual(selected.Select(t => t.Id).OrderBy(id => id));
        }
    }

    public class DeleteTourHandler : ICommandHandler<DeleteTourCommand, bool>
    {
        private readonly ITourRepository r;
        private readonly IUnitOfWork u;

        public DeleteTourHandler(ITourRepository r, IUnitOfWork u)
        {
            this.r = r;
            this.u = u;
        }

        public async Task<bool> Handle(DeleteTourCommand c, CancellationToken ct)
        {
            var x = await r.GetByIdAsync(c.Id, ct);
            if (x == null)
                throw new NotFoundException(ErrorMessages.TourNotFound, ErrorCode.TourNotFound);
            r.Remove(x);
            await u.SaveChangesAsync(ct);
            return true;
        }
    }

    public class GetTourByIdHandler : IQueryHandler<GetTourByIdQuery, TourDto>
    {
        private readonly ITourRepository r;

        public GetTourByIdHandler(ITourRepository r)
        {
            this.r = r;
        }

        public async Task<TourDto> Handle(GetTourByIdQuery q, CancellationToken ct)
        {
            var x = await r.GetByIdAsync(q.Id, ct);
            if (x == null)
                throw new NotFoundException(ErrorMessages.TourNotFound, ErrorCode.TourNotFound);
            return x.ToDto();
        }
    }

    public class GetToursHandler : IQueryHandler<GetToursQuery, PagedResponseDto<TourListItemDto>>
    {
        private readonly ITourRepository r;
        private readonly PagedQueryExecutor p;

        public GetToursHandler(ITourRepository r, PagedQueryExecutor p)
        {
            this.r = r;
            this.p = p;
        }

        public async Task<PagedResponseDto<TourListItemDto>> Handle(
            GetToursQuery q,
            CancellationToken ct
        )
        {
            q = q ?? new GetToursQuery();
            var x = QuerySpecification.For(r);
            if (!string.IsNullOrWhiteSpace(q.Search))
            {
                var s = q.Search.Trim();
                x = x.Where(t => t.Name.Contains(s));
            }
            if (q.Status.HasValue)
                x = x.Where(t => (int)t.Status == q.Status);
            if (q.CurrencyId.HasValue)
                x = x.Where(t => t.CurrencyId == q.CurrencyId);
            var z = await p.ExecuteAsync(
                x.OrderBy(t => t.Name)
                    .Select(t => new TourListItemDto
                    {
                        Id = t.Id,
                        Name = t.Name,
                        Status = (int)t.Status,
                        Revision = t.Revision,
                        CurrencyId = t.CurrencyId,
                        DateMode = (int)t.DateMode,
                        AvailableDateFrom = t.AvailableDateFrom,
                        AvailableDateTo = t.AvailableDateTo,
                    }),
                q,
                ct
            );
            return new PagedResponseDto<TourListItemDto>(z.Items, z.Page, z.PageSize, z.TotalCount);
        }
    }
}
