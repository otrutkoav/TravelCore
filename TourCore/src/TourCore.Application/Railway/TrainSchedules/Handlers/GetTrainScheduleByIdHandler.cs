using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Common.Exceptions;
using TourCore.Contracts.Railway.TrainSchedules;
using TourCore.Application.Abstractions.Persistence.Railway;
using TourCore.Application.Railway.TrainSchedules.Mapping;
using TourCore.Application.Railway.TrainSchedules.Queries;
using TourCore.Application.Common.Errors;

namespace TourCore.Application.Railway.TrainSchedules.Handlers
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
                throw new NotFoundException(ErrorMessages.TrainScheduleNotFound, ErrorCode.TrainScheduleNotFound);

            return entity.ToDto();
        }
    }
}