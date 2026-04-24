using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Common.Models;
using TourCore.Contracts.Railway.TrainSchedules;
using TourCore.Application.TrainSchedules.Mappings;
using TourCore.Application.TrainSchedules.Queries;

namespace TourCore.Application.TrainSchedules.Handlers
{
    public class GetTrainSchedulesHandler : IQueryHandler<GetTrainSchedulesQuery, ListResult<TrainScheduleListItemDto>>
    {
        private readonly ITrainScheduleRepository _trainScheduleRepository;

        public GetTrainSchedulesHandler(ITrainScheduleRepository trainScheduleRepository)
        {
            _trainScheduleRepository = trainScheduleRepository;
        }

        public async Task<ListResult<TrainScheduleListItemDto>> Handle(GetTrainSchedulesQuery query, CancellationToken cancellationToken)
        {
            var entities = await _trainScheduleRepository.ListAsync(cancellationToken);
            var items = entities.AsEnumerable();

            if (query != null && query.Filter != null)
            {
                if (query.Filter.RailwayTransferId.HasValue)
                    items = items.Where(x => x.RailwayTransferId == query.Filter.RailwayTransferId.Value);
            }

            var result = items
                .OrderBy(x => x.RailwayTransferId)
                .ThenBy(x => x.DateFrom)
                .ThenBy(x => x.DateTo)
                .Select(x => x.ToListItemDto())
                .ToArray();

            return ListResult<TrainScheduleListItemDto>.Create(result);
        }
    }
}