using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Abstractions.Persistence.Services;
using TourCore.Application.Common.Errors;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Services.ServiceTypes.Commands;

namespace TourCore.Application.Services.ServiceTypes.Handlers
{
    public class DeleteServiceTypeHandler : ICommandHandler<DeleteServiceTypeCommand, bool>
    {
        private readonly IServiceTypeRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteServiceTypeHandler(IServiceTypeRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteServiceTypeCommand command, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity == null)
                throw new NotFoundException(ErrorMessages.ServiceTypeNotFound, ErrorCode.ServiceTypeNotFound);

            _repository.Remove(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
