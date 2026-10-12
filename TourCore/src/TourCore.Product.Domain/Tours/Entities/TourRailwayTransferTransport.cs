using TourCore.Domain.Common.Exceptions;
using TourCore.Shared.Primitives;

namespace TourCore.Domain.Tours
{
    public class TourRailwayTransferTransport : Entity
    {
        public int TourRailwayTransferId { get; protected set; }
        public virtual TourRailwayTransfer TourRailwayTransfer { get; protected set; }
        public int TransportId { get; protected set; }

        protected TourRailwayTransferTransport() { }

        internal TourRailwayTransferTransport(int id)
        {
            if (id <= 0)
                throw new DomainException("Transport identifier must be positive.");
            TransportId = id;
        }
    }
}
