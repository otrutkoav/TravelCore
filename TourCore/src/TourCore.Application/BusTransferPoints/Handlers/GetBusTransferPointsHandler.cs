using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Contracts.Bus.BusTransferPoints;
using TourCore.Application.BusTransferPoints.Mappings;
using TourCore.Application.BusTransferPoints.Queries;
using TourCore.Application.Common.Models;

namespace TourCore.Application.BusTransferPoints.Handlers
{
    public class GetBusTransferPointsHandler : IQueryHandler<GetBusTransferPointsQuery, ListResult<BusTransferPointListItemDto>>
    {
        private readonly IBusTransferPointRepository _busTransferPointRepository;

        public GetBusTransferPointsHandler(IBusTransferPointRepository busTransferPointRepository)
        {
            _busTransferPointRepository = busTransferPointRepository;
        }

        public async Task<ListResult<BusTransferPointListItemDto>> Handle(GetBusTransferPointsQuery query, CancellationToken cancellationToken)
        {
            var entities = await _busTransferPointRepository.ListAsync(cancellationToken);
            var items = entities.AsEnumerable();

            if (query != null && query.Filter != null)
            {
                if (query.Filter.BusTransferId.HasValue)
                    items = items.Where(x => x.BusTransferId == query.Filter.BusTransferId.Value);

                if (query.Filter.CountryFromId.HasValue)
                    items = items.Where(x => x.CountryFromId == query.Filter.CountryFromId.Value);

                if (query.Filter.CityFromId.HasValue)
                    items = items.Where(x => x.CityFromId == query.Filter.CityFromId.Value);

                if (query.Filter.CountryToId.HasValue)
                    items = items.Where(x => x.CountryToId == query.Filter.CountryToId.Value);

                if (query.Filter.CityToId.HasValue)
                    items = items.Where(x => x.CityToId == query.Filter.CityToId.Value);
            }

            var result = items
                .OrderBy(x => x.BusTransferId)
                .ThenBy(x => x.CountryFromId)
                .ThenBy(x => x.CityFromId)
                .ThenBy(x => x.CountryToId)
                .ThenBy(x => x.CityToId)
                .Select(x => x.ToListItemDto())
                .ToArray();

            return ListResult<BusTransferPointListItemDto>.Create(result);
        }
    }
}