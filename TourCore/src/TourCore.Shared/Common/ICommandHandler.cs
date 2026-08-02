using System.Threading;
using System.Threading.Tasks;

namespace TourCore.Application.Abstractions
{
    public interface ICommandHandler<in TCommand, TResult>
    {
        Task<TResult> Handle(TCommand command, CancellationToken cancellationToken);
    }
}
