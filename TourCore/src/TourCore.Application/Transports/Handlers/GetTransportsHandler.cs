using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Common.Models;
using TourCore.Application.Transports.DTOs;
using TourCore.Application.Transports.Mappings;
using TourCore.Application.Transports.Queries;

namespace TourCore.Application.Transports.Handlers
{
    public class GetTransportsHandler : IQueryHandler<GetTransportsQuery, ListResult<TransportListItemDto>>
    {
        private readonly ITransportRepository _transportRepository;

        public GetTransportsHandler(ITransportRepository transportRepository)
        {
            _transportRepository = transportRepository;
        }

        public async Task<ListResult<TransportListItemDto>> Handle(GetTransportsQuery query, CancellationToken cancellationToken)
        {
            var entities = await _transportRepository.ListAsync(cancellationToken);
            var items = entities.AsEnumerable();

            if (query != null && query.Filter != null && !string.IsNullOrWhiteSpace(query.Filter.Search))
            {
                var search = query.Filter.Search.Trim();

                items = items.Where(x =>
                    (!string.IsNullOrWhiteSpace(x.Name) && x.Name.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    (!string.IsNullOrWhiteSpace(x.NameEn) && x.NameEn.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0));
            }

            var result = items
                .OrderBy(x => x.ShowOrder)
                .ThenBy(x => x.Name)
                .Select(x => x.ToListItemDto())
                .ToArray();

            return ListResult<TransportListItemDto>.Create(result);
        }
    }
}