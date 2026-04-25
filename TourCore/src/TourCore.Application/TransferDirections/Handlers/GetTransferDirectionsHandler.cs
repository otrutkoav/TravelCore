using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Common.Models;
using TourCore.Contracts.Transfers.TransferDirections;
using TourCore.Application.TransferDirections.Mappings;
using TourCore.Application.TransferDirections.Queries;

namespace TourCore.Application.TransferDirections.Handlers
{
    public class GetTransferDirectionsHandler : IQueryHandler<GetTransferDirectionsQuery, ListResult<TransferDirectionListItemDto>>
    {
        private readonly ITransferDirectionRepository _transferDirectionRepository;

        public GetTransferDirectionsHandler(ITransferDirectionRepository transferDirectionRepository)
        {
            _transferDirectionRepository = transferDirectionRepository;
        }

        public async Task<ListResult<TransferDirectionListItemDto>> Handle(GetTransferDirectionsQuery query, CancellationToken cancellationToken)
        {
            var entities = await _transferDirectionRepository.ListAsync(cancellationToken);
            var items = entities.AsEnumerable();

            if (query != null && query.Filter != null && !string.IsNullOrWhiteSpace(query.Filter.Search))
            {
                var search = query.Filter.Search.Trim();

                items = items.Where(x =>
                    !string.IsNullOrWhiteSpace(x.Name) &&
                    x.Name.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            var result = items
                .OrderBy(x => x.Name)
                .Select(x => x.ToListItemDto())
                .ToArray();

            return ListResult<TransferDirectionListItemDto>.Create(result);
        }
    }
}