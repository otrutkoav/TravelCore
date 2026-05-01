using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence.Services;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Services.ServiceTypes.DTOs;
using TourCore.Application.Services.ServiceTypes.Mapping;
using TourCore.Application.Services.ServiceTypes.Queries;

namespace TourCore.Application.Services.ServiceTypes.Handlers
{
    public class GetServiceTypeByIdHandler : IQueryHandler<GetServiceTypeByIdQuery, ServiceTypeDto>
    {
        private readonly IServiceTypeRepository _serviceTypeRepository;

        public GetServiceTypeByIdHandler(IServiceTypeRepository serviceTypeRepository)
        {
            _serviceTypeRepository = serviceTypeRepository;
        }

        public async Task<ServiceTypeDto> Handle(GetServiceTypeByIdQuery query, CancellationToken cancellationToken)
        {
            var entity = await _serviceTypeRepository.GetByIdAsync(query.Id, cancellationToken);
            if (entity == null)
                throw new NotFoundException("Service type was not found.");

            return entity.ToDto();
        }
    }
}