using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.ServiceTypes.DTOs;
using TourCore.Application.ServiceTypes.Mappings;
using TourCore.Application.ServiceTypes.Queries;

namespace TourCore.Application.ServiceTypes.Handlers
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