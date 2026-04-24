using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.AirClasses.Mappings;
using TourCore.Application.AirClasses.Queries;
using TourCore.Application.Common.Models;
using TourCore.Contracts.Avia.AirClasses;

namespace TourCore.Application.AirClasses.Handlers
{
    public class GetAirClassesHandler : IQueryHandler<GetAirClassesQuery, ListResult<AirClassListItemDto>>
    {
        private readonly IAirClassRepository _repository;

        public GetAirClassesHandler(IAirClassRepository repository)
        {
            _repository = repository;
        }

        public async Task<ListResult<AirClassListItemDto>> Handle(GetAirClassesQuery query, CancellationToken cancellationToken)
        {
            var entities = await _repository.ListAsync(cancellationToken);

            var result = entities
                .OrderBy(x => x.SortOrder)
                .ThenBy(x => x.Name)
                .Select(x => x.ToListItemDto())
                .ToArray();

            return ListResult<AirClassListItemDto>.Create(result);
        }
    }
}