using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Common.Exceptions;
using TourCore.Contracts.Railway.TrainSchedules;
using TourCore.Application.TrainSchedules.Mappings;
using TourCore.Application.TrainSchedules.Queries;

namespace TourCore.Application.TrainSchedules.Handlers
{
    public class GetTrainScheduleByIdHandler : IQueryHandler<GetTrainScheduleByIdQuery, TrainScheduleDto>
    {
        private readonly ITrainScheduleRepository _trainScheduleRepository;

        public GetTrainScheduleByIdHandler(ITrainScheduleRepository trainScheduleRepository)
        {
            _trainScheduleRepository = trainScheduleRepository;
        }

        public async Task<TrainScheduleDto> Handle(GetTrainScheduleByIdQuery query, CancellationToken cancellationToken)
        {
            var entity = await _trainScheduleRepository.GetByIdAsync(query.Id, cancellationToken);
            if (entity == null)
                throw new NotFoundException("Train schedule was not found.");

            return entity.ToDto();
        }
    }
}