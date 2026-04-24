using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Common.Models;
using TourCore.Application.ServiceTypes.DTOs;
using TourCore.Application.ServiceTypes.Mappings;
using TourCore.Application.ServiceTypes.Queries;

namespace TourCore.Application.ServiceTypes.Handlers
{
    public class GetServiceTypesHandler : IQueryHandler<GetServiceTypesQuery, ListResult<ServiceTypeListItemDto>>
    {
        private readonly IServiceTypeRepository _serviceTypeRepository;

        public GetServiceTypesHandler(IServiceTypeRepository serviceTypeRepository)
        {
            _serviceTypeRepository = serviceTypeRepository;
        }

        public async Task<ListResult<ServiceTypeListItemDto>> Handle(GetServiceTypesQuery query, CancellationToken cancellationToken)
        {
            var entities = await _serviceTypeRepository.ListAsync(cancellationToken);
            var items = entities.AsEnumerable();

            if (query != null && query.Filter != null)
            {
                if (!string.IsNullOrWhiteSpace(query.Filter.Search))
                {
                    var search = query.Filter.Search.Trim();

                    items = items.Where(x =>
                        (!string.IsNullOrWhiteSpace(x.Name) && x.Name.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0) ||
                        (!string.IsNullOrWhiteSpace(x.NameEn) && x.NameEn.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0) ||
                        (!string.IsNullOrWhiteSpace(x.Code) && x.Code.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0));
                }

                if (query.Filter.IsRoute.HasValue)
                    items = items.Where(x => x.IsRoute == query.Filter.IsRoute.Value);

                if (query.Filter.IsCity.HasValue)
                    items = items.Where(x => x.IsCity == query.Filter.IsCity.Value);

                if (query.Filter.HasPrimaryParameter.HasValue)
                    items = items.Where(x => x.HasPrimaryParameter == query.Filter.HasPrimaryParameter.Value);

                if (query.Filter.HasSecondaryParameter.HasValue)
                    items = items.Where(x => x.HasSecondaryParameter == query.Filter.HasSecondaryParameter.Value);

                if (query.Filter.IsQuoted.HasValue)
                    items = items.Where(x => x.IsQuoted == query.Filter.IsQuoted.Value);

                if (query.Filter.IsIndividual.HasValue)
                    items = items.Where(x => x.IsIndividual == query.Filter.IsIndividual.Value);
            }

            var result = items
                .OrderBy(x => x.Name)
                .ThenBy(x => x.Code)
                .Select(x => x.ToListItemDto())
                .ToArray();

            return ListResult<ServiceTypeListItemDto>.Create(result);
        }
    }
}