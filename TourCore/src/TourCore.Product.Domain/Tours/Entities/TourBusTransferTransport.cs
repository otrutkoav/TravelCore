using TourCore.Domain.Common.Exceptions;
using TourCore.Shared.Primitives;

namespace TourCore.Domain.Tours
{
    public class TourBusTransferTransport : Entity
    {
        public int TourBusTransferId { get; protected set; }
        public virtual TourBusTransfer TourBusTransfer { get; protected set; }
        public int TransportId { get; protected set; }

        protected TourBusTransferTransport() { }

        internal TourBusTransferTransport(int transportId)
        {
            if (transportId <= 0)
                throw new DomainException("Transport identifier must be positive.");
            TransportId = transportId;
        }
    }
}
